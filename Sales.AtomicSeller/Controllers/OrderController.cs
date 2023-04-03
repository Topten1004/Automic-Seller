using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Consts;
using Sales.AtomicSeller.Entities;

namespace Sales.AtomicSeller.Controllers
{
    /// <summary>
    /// Order controller.
    /// </summary>
    [Authorize(Policy = IdentityConsts.ADMINISTRATOR_POLICY)]
    public class OrderController : Controller
    {
        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<OrderController> logger;
        /// <summary>
        /// order service.
        /// </summary>
        private readonly IOrderService orderService;
        /// <summary>
        /// web host environment.
        /// </summary>
        private readonly IWebHostEnvironment webHostEnvironment;
        /// <summary>
        /// invoice service.
        /// </summary>
        private readonly IInvoiceService invoiceService;
        /// <summary>
        /// contructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="orderService"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="invoiceService"></param>
        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService,
            IWebHostEnvironment webHostEnvironment,
            IInvoiceService invoiceService)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.webHostEnvironment = webHostEnvironment;
            this.invoiceService = invoiceService;
        }
        /// <summary>
        /// Order list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var orders = await orderService.Get();
            return View(orders);
        }
        /// <summary>
        /// Download invoice.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadInvoice(int id)
        {
            //Get order
            var order = await orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            //Build the Invoice File Path.
            string invoicePath = Path.Combine(webHostEnvironment.WebRootPath, "invoices", order.Number + ".pdf");
            // generate order if not exist.
            if (!System.IO.File.Exists(invoicePath))
            {
                await invoiceService.Generate(order, false);
            }
            //Read the Invoice data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(invoicePath);

            // the Invoice File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(invoicePath));
        }
    }
}
