using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Config;
using Sales.AtomicSeller.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Sales.AtomicSeller.Areas.Identity.Pages.Account.Manage
{
    public class OrderModel : PageModel
    {
        private readonly IEmailService _emailSender;
        private readonly IOrderService orderService;
        private readonly IInvoiceService invoiceService;
        private readonly IRootConfig rootConfig;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OrderModel(
            IEmailService emailSender,
            IOrderService appOrderService,
            IInvoiceService invoiceService,
            IRootConfig rootConfig,
            IWebHostEnvironment webHostEnvironment)
        {
            _emailSender = emailSender;
            this.orderService = appOrderService;
            this.invoiceService = invoiceService;
            this.rootConfig = rootConfig;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            this.Orders = await orderService.GetMine();
            return Page();
        }
        public async Task<IActionResult> OnGetDownloadInvoice(int id)
        {
            //check user permission
            var order = await orderService.GetMine(id);
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

            //Send the Invoice File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(invoicePath));
        }


    }
}
