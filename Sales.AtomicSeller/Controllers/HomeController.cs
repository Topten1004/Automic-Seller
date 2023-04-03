using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Business.Services;
using Sales.AtomicSeller.Helpers;
using Sales.AtomicSeller.Models;
using System.Diagnostics;
using System.Globalization;

namespace Sales.AtomicSeller.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly IAtomicServiceService AtomicServiceService;

        public HomeController(ILogger<HomeController> logger,
            IAtomicServiceService AtomicServiceService)
        {
            _logger = logger;
            this.AtomicServiceService = AtomicServiceService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await AtomicServiceService.Get(a => a.Visible);
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            AtomicServiceViewModel _AtomicServiceViewModel = new AtomicServiceViewModel();

            var _AtomicService = await AtomicServiceService.Get(id);

            _AtomicServiceViewModel.AtomicService = _AtomicService;

            var _Addons = await AtomicServiceService.Get(a => a.Visible && a.ServiceType=="ADDON");

            _AtomicServiceViewModel.AddOns = (List<Entities.AtomicService>)_Addons;

            return View(_AtomicServiceViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            // Create Cookie / Replace Cookie if already exists
            CookieOptions MyCookie = new CookieOptions();
            MyCookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Response.Cookies.Append("_culture", culture, MyCookie);

            //Get cookie value : var cookieVal = HttpContext.Request.Cookies["_culture"];

            //CultureHelper.ChangeCurrentCulture(culture);
            // Second way : Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            string referer = Request.Headers["Referer"].ToString();
            if (referer != null &&
                referer != null &&
                !string.IsNullOrWhiteSpace(referer))
            {
                return Redirect(referer);
            }

            return Redirect("/Home/Index");
            //return Redirec tToAction("Index", "Home");
        }

    }
}