using AutoMapper;
using CommonItems.Exceptions;
using EventBus.Messaging.Events;
using LawEnforcement.Application.Contracts;
using MassTransit;

namespace LawEnforcementAPI.EventBusConsumer
{
    public class CrimeEventConsumer : IConsumer<CrimeEvent>
    {
        private readonly ILogger<CrimeEventConsumer> _logger;
        private readonly IMapper _mapper;
        private readonly ICrimeEventRepository _crimeRepository;
        private readonly IEnforcementRepository _enfRepository;

        public CrimeEventConsumer(
            ILogger<CrimeEventConsumer> logger, 
            IMapper mapper, 
            ICrimeEventRepository crimeRepository, 
            IEnforcementRepository enfRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _crimeRepository = crimeRepository;
            _enfRepository = enfRepository;
        }

        public async Task Consume(ConsumeContext<CrimeEvent> context)
        {
            var crime = context.Message;

            await UpdateEnforcement(crime);
            _logger.LogInformation($"Crime {crime.Id} consumed successfully.");
        }

        private async Task UpdateEnforcement(CrimeEvent crime)
        {
            var enforcementToUpdate = await _enfRepository.GetByIdAsync(crime.EnforcementId);

            if (enforcementToUpdate == null)
                throw new NotFoundException("No such id in the database");

            await _crimeRepository.AddAsync(crime);
            await _crimeRepository.Save();

            enforcementToUpdate.Crimes.ToList().Add(crime);
            await _enfRepository.Save();
        }
    }
}
