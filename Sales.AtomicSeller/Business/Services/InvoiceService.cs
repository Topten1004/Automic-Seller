using DinkToPdf;
using DinkToPdf.Contracts;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;
using Sales.AtomicSeller.Config;
using System.Text;

namespace Sales.AtomicSeller.Business.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailService emailSender;
        private readonly IConverter converter;
        private readonly IRootConfig rootConfig;
        private readonly string basePath = String.Empty;

        public InvoiceService(ApplicationDbContext applicationDbContext,
            IWebHostEnvironment webHostEnvironment,
            IEmailService emailSender,
            IConverter converter,
            IRootConfig rootConfig)
        {
            this.applicationDbContext = applicationDbContext;
            this.webHostEnvironment = webHostEnvironment;
            basePath = this.webHostEnvironment.WebRootPath;
            this.emailSender = emailSender;
            this.converter = converter;
            this.rootConfig = rootConfig;
        }

        public async Task<bool> Generate(Order order, bool sendByEmail)
        {
            var user = await applicationDbContext.Users.FindAsync(order.UserId);
            string body = string.Empty;
            string orderDetailsToBeRendered = string.Empty;
            decimal invoiceSubTotal = 0;
            decimal invoiceTotal = 0;
            decimal invoiceDiscount = 0;
            decimal taxRate = 0;
            decimal tax = 0;
            try
            {
                var pathToInvoice = Path.Combine(basePath, "invoices", "templates", "invoice.html");
                var pathToInvoiceRow = File.ReadAllText(Path.Combine(basePath, "invoices", "templates", "invoice_row.html"));
                using (StreamReader reader = new StreamReader(pathToInvoice))
                {
                    body = reader.ReadToEnd();
                }
                foreach (var orderDetails in order.OrderDetails)
                {
                    orderDetailsToBeRendered += pathToInvoiceRow.Replace("@ProductName", $"{orderDetails.Product.ServiceName}")
                                                        .Replace("@ProductUnitPrice", orderDetails.UnitPrice.ToString())
                                                        .Replace("@ProductQuantity", orderDetails.Quantity.ToString())
                                                        .Replace("@ProductTotalPrice", (orderDetails.Quantity * orderDetails.UnitPrice).ToString());
                    invoiceSubTotal += (orderDetails.Quantity * orderDetails.UnitPrice);
                }
                tax = Math.Round((invoiceSubTotal * rootConfig.InvoiceConfig.TaxRate) / 100, 2);
                taxRate = rootConfig.InvoiceConfig.TaxRate;
                invoiceTotal = Math.Round(invoiceSubTotal + tax - invoiceDiscount, 2);

                body = body.Replace("@Img", rootConfig.InvoiceConfig.Logo);
                body = body.Replace("@CompanyName", rootConfig.InvoiceConfig.Name);
                body = body.Replace("@CompanyAddress1", rootConfig.InvoiceConfig.Address);
                body = body.Replace("@CompanyAddress2", rootConfig.InvoiceConfig.AddressComplement);

                body = body.Replace("@CompanyCity", rootConfig.InvoiceConfig.City);
                body = body.Replace("@CompanyCountry", rootConfig.InvoiceConfig.Country);

                body = body.Replace("@Email", user.Email);
                body = body.Replace("@Website", rootConfig.InvoiceConfig.Website);
                body = body.Replace("@CustomerName", $"{user.FirstName} {user.LastName}{(string.IsNullOrWhiteSpace(user.Company) ? "" : "<br/>" + user.Company)}");
                body = body.Replace("@CustomerAddress1", $"{user.StreetAddress1}{(string.IsNullOrWhiteSpace(user.StreetAddress2) ? "" : "<br/>" + user.StreetAddress2)}");
                body = body.Replace("@CustomerCountry", user.Country);

                body = body.Replace("@CustomerTownCity", user.City);
                body = body.Replace("@CustomerPostCodeZip", user.PostCodeZip);
                body = body.Replace("@CustomerPhoneNumber", user.PhoneNumber);

                body = body.Replace("@InvoiceNumber", order.Number);
                body = body.Replace("@IssueDate", order.CreatedOn.ToString("MMMM dd, yyyy"));
                body = body.Replace("@ProductTR", orderDetailsToBeRendered);
                body = body.Replace("@SubTotal", invoiceSubTotal.ToString());
                body = body.Replace("@Discount", invoiceDiscount.ToString());
                body = body.Replace("@TaxRate", taxRate.ToString());
                body = body.Replace("@Tax", tax.ToString());
                body = body.Replace("@InvoiceTotal", invoiceTotal.ToString());

                if (GeneratePdfReport(body, order.Number, out string pdfFilepath))
                {
                    if (sendByEmail)
                    {
                        await Send(order, user, invoiceTotal, pdfFilepath);
                    }
                    return true;
                }
                else
                {
                    return false; // error on pdf invoice
                }
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
        }

        private bool GeneratePdfReport(string html, string invoiceNumber, out string pdfFilepath)
        {
            pdfFilepath = string.Empty;
            bool result = true;
            try
            {
                if (File.Exists(Path.Combine(basePath, "Invoices", invoiceNumber + ".pdf")))
                {
                    pdfFilepath = Path.Combine(basePath, "Invoices", invoiceNumber + ".pdf");
                    return true;
                }
                GlobalSettings globalSettings = new GlobalSettings();
                globalSettings.ColorMode = ColorMode.Color;
                globalSettings.Orientation = Orientation.Portrait;
                globalSettings.PaperSize = PaperKind.A4;
                globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25, Left = 21, Right = 21 };
                ObjectSettings objectSettings = new ObjectSettings();
                objectSettings.PagesCount = true;
                objectSettings.HtmlContent = html;
                WebSettings webSettings = new WebSettings();
                webSettings.DefaultEncoding = "utf-8";
                objectSettings.WebSettings = webSettings;
                HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings },
                };

                byte[] pdfContent = converter.Convert(htmlToPdfDocument);
                string filePath = Path.Combine(basePath, "Invoices", invoiceNumber + ".pdf");
                File.WriteAllBytes(filePath, pdfContent);
                pdfFilepath = filePath;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        private async Task<bool> Send(Order appOrder, ApplicationUser user, decimal invoiceTTC, string invoicePdfFielPath)
        {
            try
            {
                StringBuilder confirmationEmail = new StringBuilder();
                confirmationEmail.AppendLine($"Hello {user.FirstName},\n");
                confirmationEmail.AppendLine($"Woo hoo! Your order is on its way.Your order details can be found below.");
                confirmationEmail.AppendLine($"ORDER SUMMARY:");
                confirmationEmail.AppendLine($"Order #: {appOrder.Number}");
                confirmationEmail.AppendLine($"Order Date: {appOrder.CreatedOn}");
                confirmationEmail.AppendLine($"Order Total: {appOrder.Total}");
                await emailSender.SendEmailAsync(user.Email, "Thank You For Your Order", confirmationEmail.ToString(), new List<string>() { invoicePdfFielPath });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<decimal> CalculateTax(decimal total)
        {
            return Math.Round((total * rootConfig.InvoiceConfig.TaxRate) / 100, 2);
        }
    }
}
