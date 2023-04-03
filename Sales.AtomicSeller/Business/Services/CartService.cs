using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Helpers;
using Sales.AtomicSeller.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sales.AtomicSeller.Business.Services
{
    public class CartService : Repository<Cart>, ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAtomicServiceService productService;

        public CartService(ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor,
            IAtomicServiceService productService)
            : base(applicationDbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.productService = productService;
        }

        public async ValueTask<Cart> GetBySessionId(string sessionId)
        {
            var cart = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                                                       && c.SessionId == sessionId)
                                              .Include(c => c.CartItems)
                                              .ThenInclude(c => c.AtomicService)
                                              .Include(c => c.ApplicationUser)
                                              .FirstOrDefaultAsync();
            return cart;
        }
        public async ValueTask<Cart> GetByTransactionId(string transactionId)
        {
            var cart = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                                                       && c.TransactionId == transactionId)
                                              .Include(c => c.CartItems)
                                              .ThenInclude(c => c.AtomicService)
                                              .Include(c => c.ApplicationUser)
                                              .FirstOrDefaultAsync();
            return cart;
        }
        public async ValueTask<Cart> SetSessionId(int id, string sessionId)
        {
            var cart = await base.Get(id);
            if (cart != null)
            {
                cart.SessionId = sessionId;
                cart.UpdatedOn = DateTime.Now;
                await base.Update(cart);
                await base.SaveChange();
            }
            return cart;
        }
        public async ValueTask<Cart> SetTransactionId(int id, string transactionId)
        {
            var cart = await base.Get(id);
            if (cart != null && cart.TransactionId == null)
            {
                cart.TransactionId = transactionId;
                cart.UpdatedOn = DateTime.Now;
                await base.Update(cart);
                await base.SaveChange();
            }
            return cart;
        }
        public async ValueTask<Cart> GetCurrent(bool createIfNotExist)
        {
            // get session cart
            var sessionCart = httpContextAccessor?.HttpContext?.Session.GetObjectFromJson<Cart>("cart");

            if (!string.IsNullOrWhiteSpace(this.Context.IdentityProvider?.UserId))
            {
                //Get current user cart from database

                /*
                var Test1 = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                && c.TransactionId == null).FirstOrDefaultAsync();

                IQueryable query = Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId && c.TransactionId == null).Include(ci => ci.CartItems);

                string sql1 = query.ToQueryString();


                var Test2 = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                                                           && c.TransactionId == null)
                                                  .Include(c => c.CartItems)
                                                  .FirstOrDefaultAsync();

                var Test3 = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                                           && c.TransactionId == null)
                                  .Include(c => c.CartItems)
                                  .ThenInclude(c => c.AtomicService)
                                  .FirstOrDefaultAsync();

                */
                var userCart = await Context.Carts.Where(c => c.UserID == Context.IdentityProvider.UserId
                                                           && c.TransactionId == null)
                                                  .Include(c => c.CartItems)
                                                  .ThenInclude(c => c.AtomicService)
                                                   .Include(c => c.ApplicationUser)
                                                  .FirstOrDefaultAsync();

                //if current user cart is null or empty, update it from session cart
                if (userCart == null && sessionCart != null)
                {
                    userCart = new Cart()
                    {
                        UserID = Context.IdentityProvider.UserId,
                        CartItems = new List<CartItem>(),
                        CreatedOn = DateTime.Now,
                    };
                    await base.Add(userCart);
                    foreach (var item in sessionCart.CartItems)
                    {
                        userCart.CartItems.Add(new CartItem() { ProductId = item.ProductId, Quantity = item.Quantity });
                    }
                    await base.SaveChange();
                    userCart = await Context.Carts.Where(c => c.Id == userCart.Id)
                                                  .Include(c => c.CartItems)
                                                  .ThenInclude(c => c.AtomicService)
                                                   .Include(c => c.ApplicationUser)
                                                  .FirstOrDefaultAsync();

                    // remove empty cart
                    httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", null);
                }
                if (userCart == null && createIfNotExist)
                {
                    userCart = new Cart()
                    {
                        UserID = Context.IdentityProvider.UserId,
                        CartItems = new List<CartItem>(),
                        CreatedOn = DateTime.Now
                    };
                    await base.Add(userCart);
                    await base.SaveChange();
                }
                return userCart;
            }
            else
            {
                if (sessionCart == null && createIfNotExist)
                {
                    sessionCart = new Cart();
                    sessionCart.CartItems = new List<CartItem>();
                    httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", sessionCart);
                }
                return sessionCart;
            }
        }
        public async ValueTask<int> GetCurrentCount()
        {
            var cart = await GetCurrent(false);
            if (cart == null)
            {
                return 0;
            }
            else
            {
                return cart.CartItems.Count;
            }
        }
        public async ValueTask<Cart> RemoveCartItem(int productId)
        {
            // get cart
            var cart = await GetCurrent(false);
            if (cart != null)
            {
                // is database cart
                if (cart.Id > 0)
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index >= 0)
                    {
                        cart.CartItems.RemoveAt(index);
                        await base.Update(cart);
                        await base.SaveChange();
                    }
                }
                // is session cart
                else
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index >= 0)
                    {
                        cart.CartItems.RemoveAt(index);
                        httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", cart);
                    }
                }
            }
            return cart;
        }
        public async ValueTask<Cart> AddCartItem(int productId, bool monthOrAnnaul)
        {
            var product = await productService.Get(productId);
            if(!monthOrAnnaul)
            {
                double price = Decimal.ToDouble(product.UnitPriceExclTax) * 0.1;
                product.UnitPriceExclTax = Convert.ToDecimal(price);
            }

            // get cart
            var cart = await GetCurrent(true);

            if (cart != null)
            {
                // is database cart
                if (cart.Id > 0)
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index >= 0)
                    {
                        cart.CartItems[index].Quantity++;
                    }
                    else
                    {
                        cart.CartItems.Add(new CartItem()
                        {
                            AtomicService = product,
                            ProductId = productId,
                            Quantity = 1
                        });
                    }
                    await base.Update(cart);
                    await base.SaveChange();
                }
                // is session cart
                else
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index != -1)
                    {
                        cart.CartItems[index].Quantity++;
                    }
                    else
                    {
                        cart.CartItems.Add(new CartItem { AtomicService = product, ProductId = productId, Quantity = 1 });
                    }
                    httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", cart);
                }
            }
            return cart;
        }
        public async ValueTask<Cart> UpCartItemQuantity(int productId)
        {
            // get cart
            var cart = await GetCurrent(false);

            if (cart != null)
            {
                // is database cart
                if (cart.Id > 0)
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index >= 0)
                    {
                        cart.CartItems[index].Quantity++;
                    await base.Update(cart);
                    await base.SaveChange();
                    }
                }
                // is session cart
                else
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index != -1)
                    {
                        cart.CartItems[index].Quantity++;
                    }
                    httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", cart);
                }
            }
            return cart;
        }
        public async ValueTask<Cart> DownCartItemQuantity(int productId)
        {
            // get cart
            var cart = await GetCurrent(false);

            if (cart != null)
            {
                // is database cart
                if (cart.Id > 0)
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index >= 0)
                    {
                        cart.CartItems[index].Quantity--;
                    }
                    if (cart.CartItems[index].Quantity == 0)
                    {
                        cart.CartItems.RemoveAt(index);
                    }
                    await base.Update(cart);
                    await base.SaveChange();
                }
                // is session cart
                else
                {
                    int index = cart.CartItems.ToList().FindIndex(i => i.AtomicService.Id == productId);
                    if (index != -1)
                    {
                        cart.CartItems[index].Quantity--;
                    }
                    if (cart.CartItems[index].Quantity == 0)
                    {
                        cart.CartItems.RemoveAt(index);
                    }
                    httpContextAccessor?.HttpContext?.Session.SetObjectAsJson("cart", cart);
                }
            }
            return cart;
        }
    }
}
