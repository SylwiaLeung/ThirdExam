using EventBus.Messaging.Events;
using MailingService.Models;
using MailingService.Services;
using MassTransit;

namespace MailingService.EventBusConsumer
{
    public class CrimeUpdateConsumer : IConsumer<CrimeUpdateEvent>
    {
        private readonly ILogger<CrimeUpdateConsumer> _logger;
        private readonly IMailService _emailService;

        public CrimeUpdateConsumer(ILogger<CrimeUpdateConsumer> logger, IMailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<CrimeUpdateEvent> context)
        {
            var crime = context.Message;
            _logger.LogInformation($"Crime {crime.Id} consumed successfully. Sending mail to the witness...");

            await SendMail(crime);
        }

        private async Task SendMail(CrimeUpdateEvent crime)
        {
            var email = new Email() 
            { 
                ToAddress = $"{crime.WitnessEmail}", 
                Body = $"Hello, {crime.WitnessName}!\nThe crime you have reported has changed its status.\nCurrent status: {crime.Status}", 
                Subject = "Change of crime status", 
                ToName = $"{crime.WitnessName}" 
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Crime report failed on sending due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
