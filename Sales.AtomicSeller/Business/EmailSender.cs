using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System;
using Sales.AtomicSeller.Config;

namespace Sales.AtomicSeller.Business
{
    public class EmailSender : IEmailSender
    {
        private IRootConfig configuration;
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(IRootConfig iConfig, ILogger<EmailSender> logger)
        {
            configuration = iConfig;
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendEmailAsync(email, subject, htmlMessage, null);
        }
        /// <summary>
        /// Send Email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <param name="alternateView"></param>
        /// <returns></returns>
        public async Task<string> SendEmailAsync(string email, string subject, string htmlMessage, AlternateView alternateView)
        {
            var sent = "Sent";
            try
            {
                var fromAddress = new MailAddress(configuration.EmailCredConfig.From, configuration.EmailCredConfig.FromName);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                NetworkCredential networkCredentials = new NetworkCredential();
                networkCredentials.UserName = fromAddress.Address;
                networkCredentials.Password = configuration.EmailCredConfig.Password;
                var smtp = new SmtpClient
                {
                    Host = configuration.SMTPConfig.Host,
                    Port = configuration.SMTPConfig.Port,
                    EnableSsl = configuration.SMTPConfig.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = configuration.SMTPConfig.UseDefaultCredentials,
                    Credentials = networkCredentials
                };
                var message = new MailMessage();
                message.From = fromAddress;
                string[] listOfEmails = null;
                if (email.Length > 0)
                {
                    listOfEmails = email.Split(',');
                    foreach (var singleEmail in listOfEmails)
                    {
                        message.To.Add(new MailAddress(singleEmail));
                    }
                    message.Subject = subject;
                    message.Body = htmlMessage;
                    message.IsBodyHtml = true;
                    if (alternateView != null)
                        message.AlternateViews.Add(alternateView);
                    {
                        smtp.Send(message);
                    }
                }
                else
                {
                    string msg = "Error: User email not found";
                    sent = msg;
                    _logger.LogError(msg, String.Format("Error! {0} | {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, this.GetType().Name));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, String.Format("Error! {0} | {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, this.GetType().Name));
                sent = "Error " + ex.Message;
            }
            return sent;
        }
    }
}
