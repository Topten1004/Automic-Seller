using DinkToPdf;
using DinkToPdf.Contracts;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Sales.AtomicSeller.Business.Services
{
    public class OrderService : Repository<Order>, IOrderService
    {
        public OrderService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
        public async Task<Order> Genereate(Cart cart, string transactionId)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                Order order = null;
                try
                {
                    // If order already exist with this transaction Id
                    var existingOrder = Context.Orders.Where(o => o.PaymentTransactionId == transactionId).Include(o => o.OrderDetails).FirstOrDefault();
                    if (existingOrder != null)
                    {
                        return existingOrder;
                    }
                    if (cart.CartItems.Count > 0)
                    {
                        // create the order
                        order = new Order();
                        order.Number = DateTime.Now.Ticks.ToString();
                        order.PaymentSource = Enums.PaymentSourceEnum.Stripe;
                        order.CreatedOn = DateTime.Now;
                        order.UserId = cart.UserID;
                        order.Total = cart.Total;
                        order.PaymentTransactionId = transactionId;
                        order.Currency = "USD";

                        Context.Add(order);
                        Context.SaveChanges();
                        // Create an Order Details Item per each Product on the Shopping Cart.
                        foreach (var item in cart.CartItems)
                        {
                            OrderDetails orderDetails = new OrderDetails();
                            orderDetails.OrderId = order.Id;
                            orderDetails.ProductId = item.ProductId;
                            orderDetails.Quantity = item.Quantity;
                            orderDetails.UnitPrice = item.AtomicService.UnitPriceExclTax;

                            Context.Add(orderDetails);
                            Context.SaveChanges();
                        }

                        await transaction.CommitAsync();
                    }
                    return order;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public async Task<List<Order>> GetMine()
        {
            return await Context.Orders.Where(o => o.UserId == Context.IdentityProvider.UserId)
                                       .Include(o => o.OrderDetails)
                                       .ThenInclude(o => o.Product)
                                       .ToListAsync();
        }
        public async Task<Order> GetMine(int id)
        {
            return await Context.Orders.Where(o => o.Id == id && o.UserId == Context.IdentityProvider.UserId)
                                       .Include(o => o.OrderDetails)
                                       .ThenInclude(o => o.Product)
                                       .FirstOrDefaultAsync();
        }
        public async override Task<IEnumerable<Order>> Get()
        {
            return await Context.Orders.Include(o => o.OrderDetails)
                                       .ThenInclude(o => o.Product)
                                       .ToListAsync();
        }
        public async Task<List<Order>> GetAll()
        {
            return await Context.Orders.Include(o => o.OrderDetails)
                                       .ThenInclude(o => o.Product)
                                       .Include(o => o.User)
                                       .ToListAsync();
        }
    }
}
