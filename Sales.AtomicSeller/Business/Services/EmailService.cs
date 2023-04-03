using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System;
using Sales.AtomicSeller.Config;
using Sales.AtomicSeller.Business.IServices;

namespace Sales.AtomicSeller.Business.Services
{
    public class EmailService : IEmailService
    {
        private IRootConfig configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IRootConfig iConfig, ILogger<EmailService> logger)
        {
            configuration = iConfig;
            _logger = logger;
        }
        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage, List<string> attachmentPaths)
        {
            try
            {
                var fromAddress = new MailAddress(configuration.EmailCredConfig.From, configuration.EmailCredConfig.FromName);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                NetworkCredential networkCredentials = new NetworkCredential();
                networkCredentials.UserName = fromAddress.Address;
                networkCredentials.Password = configuration.EmailCredConfig.Password;
                var SmtpClient = new SmtpClient
                {
                    Host = configuration.SMTPConfig.Host,
                    Port = configuration.SMTPConfig.Port,
                    EnableSsl = configuration.SMTPConfig.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = configuration.SMTPConfig.UseDefaultCredentials,
                    Timeout = 8000,
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
                    if (attachmentPaths != null)
                    {
                        foreach (var path in attachmentPaths)
                        {
                            if (!string.IsNullOrWhiteSpace(path))
                            {
                                // Create  the file attachment for this email message.
                                Attachment attachment = new Attachment(path);
                                // Add the file attachment to this email message.
                                message.Attachments.Add(attachment);
                            }
                        }
                    }
                    try {
                        SmtpClient.Send(message);
                    }
                    catch (Exception ex)
                    { 

                    }                    
                    
                    return true;
                }
                else
                {
                    _logger.LogError("Error: User email not found");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
