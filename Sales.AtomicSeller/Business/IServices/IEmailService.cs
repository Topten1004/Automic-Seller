using System.Net.Mail;

namespace Sales.AtomicSeller.Business.IServices
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage, List<string> attachmentPaths );
    }
}