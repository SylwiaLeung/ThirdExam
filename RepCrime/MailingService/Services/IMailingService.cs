using MailingService.Models;

namespace MailingService.Services
{
    public interface IMailingService
    {
        Task<bool> SendEmail(Email email);
    }
}
