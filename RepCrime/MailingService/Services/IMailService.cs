using MailingService.Models;

namespace MailingService.Services
{
    public interface IMailService
    {
        Task<bool> SendEmail(Email email);
    }
}
