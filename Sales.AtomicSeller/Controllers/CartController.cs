using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Consts;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Sales.AtomicSeller.Controllers
{
    /// <summary>
    /// shopping cart controller.
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// Logger.
        /// </summary>Buy
        private readonly ICartService cartService;
        /// <summary>
        /// Cart service.
        /// </summary>
        private readonly ILogger<CartController> logger;
      /// <summary>
      /// User manager.
      /// </summary>
        private readonly UserManager<ApplicationUser> userManager;
        /// <summary>
        /// Sign in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> signInManager;
        /// <summary>
        /// Web host environment.
        /// </summary>
        private readonly IWebHostEnvironment webHostEnvironment;
        /// <summary>
        /// Constrcutor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cartService"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="webHostEnvironment"></param>
        public CartController(ILogger<CartController> logger,
            ICartService cartService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.cartService = cartService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Return shopping cart view.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cart = await cartService.GetCurrent(false);
            return View(cart);
        }
        /// <summary>
        /// Buy new product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            await cartService.AddCartItem(id, true);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Buy new products.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuyItems(int[] ids)
        {
            if (ids[1] == 0)
            {
                await cartService.AddCartItem(ids[0], false);
            } else
            {
                await cartService.AddCartItem(ids[0], true);
            }

            for(int i = 2; i < ids.Length; i++)
            {
                await cartService.AddCartItem(ids[i], true);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Remove product from shopping cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            Cart cart = await cartService.RemoveCartItem(id);
            return PartialView("_Cart", cart);
        }
        /// <summary>
        /// Up product quantity on shopping cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpQuantity(int id)
        {
            try
            {
                Cart cart = await cartService.UpCartItemQuantity(id);
                return PartialView("_Cart", cart);
            }
            catch (Exception ex)
            {
                return await Task.Run(() => StatusCode(StatusCodes.Status500InternalServerError, Json(new { ex.Message })));
            }
        }
        /// <summary>
        /// Down product quantity on shopping cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DownQuantity(int id)
        {
            try
            {
                Cart cart = await cartService.DownCartItemQuantity(id);
                return PartialView("_Cart", cart);
            }
            catch (Exception ex)
            {
                return await Task.Run(() => StatusCode(StatusCodes.Status500InternalServerError, Json(new { ex.Message })));
            }
        }
        /// <summary>
        /// Checkout view.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = IdentityConsts.USER_POLICY)]
        public async Task<IActionResult> Checkout()
        {
            var countries = System.IO.File.ReadAllLines(Path.Combine(webHostEnvironment.WebRootPath, "countries.txt"));
            ViewBag.Countries = countries;
            var cart = await cartService.GetCurrent(false);
            return View(cart);
        }
        /// <summary>
        /// Post user informations before payment.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(Policy = IdentityConsts.USER_POLICY)]
        [HttpPost]
        public async Task<IActionResult> Checkout(ApplicationUser user)
        {
            try
            {
                var appUser = await userManager.GetUserAsync(User);
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;
                appUser.Company = user.Company;
                appUser.StreetAddress1 = user.StreetAddress1;
                appUser.StreetAddress2 = user.StreetAddress2;
                appUser.Country= user.Country;
                appUser.PostCodeZip = user.PostCodeZip;
                appUser.City = user.City;
                appUser.PhoneNumber = user.PhoneNumber;
                await userManager.UpdateAsync(appUser);
                await signInManager.RefreshSignInAsync(appUser);
                return RedirectToAction("Index", "Checkout");
            }
            catch (Exception)
            {
                ViewBag.Error = "Error updating user informations.";
                var cart = await cartService.GetCurrent(false);
                return View(cart);
            }
        }
    }
}
