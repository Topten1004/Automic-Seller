using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Config;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Helpers;
using Sales.AtomicSeller.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
using Sales.AtomicSeller.Consts;

namespace Sales.AtomicSeller.Controllers
{
    /// <summary>
    /// Checkout with stripe controller.
    /// </summary>
    [Authorize(Policy = IdentityConsts.USER_POLICY)]
    public class CheckoutController : Controller
    {
        /// <summary>
        /// root config.
        /// </summary>
        private readonly IRootConfig rootConfig;
        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<CheckoutController> logger;
        /// <summary>
        /// order service.
        /// </summary>
        private readonly IOrderService orderService;
        /// <summary>
        /// invoice service.
        /// </summary>
        private readonly IInvoiceService invoiceService;
        /// <summary>
        /// cart service.
        /// </summary>
        private readonly ICartService cartService;
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="rootConfig"></param>
        /// <param name="logger"></param>
        /// <param name="orderService"></param>
        /// <param name="invoiceService"></param>
        /// <param name="cartService"></param>
        public CheckoutController(IRootConfig rootConfig,
            ILogger<CheckoutController> logger,
            IOrderService orderService,
            IInvoiceService invoiceService,
            ICartService cartService)
        {
            this.rootConfig = rootConfig;
            this.logger = logger;
            this.orderService = orderService;
            this.invoiceService = invoiceService;
            this.cartService = cartService;
        }
        /// <summary>
        /// Total amount of cart.
        /// </summary>
        [TempData]
        public string TotalAmount { get; set; }
        /// <summary>
        /// Checkout cart with stripe.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var cart = await cartService.GetCurrent(false);
                TotalAmount = cart.Total.ToString();
                string url = "https://" + HttpContext.Request.Host.Value + "/Checkout/Complete?session_id={CHECKOUT_SESSION_ID}";
                var optionsSession = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card", },
                    LineItems = new List<SessionLineItemOptions> {
                         new SessionLineItemOptions {
                             PriceData = new SessionLineItemPriceDataOptions
                                    {
                                      Currency = "usd",
                                      UnitAmountDecimal = Convert.ToInt64(cart.Total * 100),
                                      ProductData = new SessionLineItemPriceDataProductDataOptions
                                      {
                                        Name = "SC Stripe Shopping Store",
                                        Description = "SC Stripe Shopping Store",
                                      },
                                    },Quantity=1
                             },
                         },

                    Mode = "payment",
                    SuccessUrl = url,
                    CancelUrl = url,
                };

                var serviceSession = new SessionService();
                Session session = serviceSession.Create(optionsSession);
                await cartService.SetSessionId(cart.Id, session.Id);
                HttpContext.Session.SetString("StripeId", session.Id);
                ViewBag.StripeId = session.Id;
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError("Checkout Error: " + ex);
                return RedirectToAction("Error", "Home");
            }
        }
        /// <summary>
        /// oOn checkout complete, show invoice.
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Complete(string session_id)
        {
            PaymentCompleteViewModel paymentCompleteViewModel = new PaymentCompleteViewModel();
            try
            {
                var sessionService = new SessionService();
                Session session = sessionService.Get(session_id);

                if (session.PaymentIntentId != null)
                {

                    var paymentIntents = new PaymentIntentService();
                    var intent = paymentIntents.Get(session.PaymentIntentId);

                    if (intent.Status == "succeeded")
                    {
                        paymentCompleteViewModel.SuccessufulPayment = true;
                        var cart = await cartService.GetBySessionId(session_id);
                        await cartService.SetTransactionId(cart.Id, session.PaymentIntentId);
                        var order = await orderService.Genereate(cart, session.PaymentIntentId);
                        paymentCompleteViewModel.Order = order;
                        paymentCompleteViewModel.Cart = cart;
                        if (order == null)
                        {
                            paymentCompleteViewModel.Message = "Your Payment is Successfull. Something wents wrong while generating order. You will recieve email when issue resolved.";
                        }
                        else
                        {
                            if (!await invoiceService.Generate(order, true))
                            {
                                paymentCompleteViewModel.Message = "Your Payment is Successfull. Something wents wrong while generating invoice. You will recieve email when issue resolved.";
                            }
                        }
                    }
                    else
                    {
                        paymentCompleteViewModel.Message = "Transaction Status is: " + intent.Status.Replace(" ", "_");
                    }
                }
                else
                    paymentCompleteViewModel.Message = "Stripe Error:  No Valid Transaction";
            }
            catch (Exception ex)
            {
                logger.LogError("Stripe Checkout Complete Error: " + ex);
                paymentCompleteViewModel.Message = "Something wents wrong. Please try again later";
            }
            return View(paymentCompleteViewModel);

        }
    }
}
