using AutoMapper;
using CommonItems.Enums;
using CommonItems.Exceptions;
using CommonItems.Models;
using EventBus.Messaging.Events;
using LawEnforcement.Application.Contracts;
using LawEnforcement.Domain.DTO;
using LawEnforcement.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LawEnforcementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnforcementController : ControllerBase
    {
        private readonly IEnforcementRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICrimeRepository _crimeRepo;
        private readonly IPublishEndpoint _publishEndpoint;

        public EnforcementController(IEnforcementRepository repository, ICrimeRepository crimeRepo, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _crimeRepo = crimeRepo;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultModel<EnforcementReadDto>>> GetAllEnforcement([FromQuery] QueryModel query)
        {
            if (query.PageNumber == 0 || query.PageSize == 0)
                throw new BadRequestException("You must specify page number and page size.");

            var enforcement = await _repository.GetAllAsync();
            var enforcementDtos = _mapper.Map<List<EnforcementReadDto>>(enforcement);
            
            var baseQuery = enforcementDtos
                .Where(e => query.SearchPhrase == null
                || e.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                || e.Rank.ToString().ToLower().Contains(query.SearchPhrase.ToLower()));

            var filteredDtos = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var result = new PagedResultModel<EnforcementReadDto>(filteredDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnforcementReadDto>> GetEnforcementById(string id)
        {
            var enforcement = await _repository.GetByIdAsync(id);

            if (enforcement == null)
                throw new NotFoundException("---> Error: No such id in the database");

            return Ok(_mapper.Map<EnforcementReadDto>(enforcement));
        }

        [HttpPost]
        public async Task<ActionResult<EnforcementReadDto>> CreateEnforcementUnit(EnforcementCreateDto enforcement)
        {
            var enforcementModel = _mapper.Map<Enforcement>(enforcement);

            await _repository.AddAsync(enforcementModel);
            await _repository.Save();

            return Ok(_mapper.Map<EnforcementReadDto>(enforcementModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEnforcementStatus(string id)
        {
            var crimeToUpdate = await _crimeRepo.GetByIdAsync(id);

            if (crimeToUpdate == null)
                throw new NotFoundException("No such id in the database");

            crimeToUpdate.Status = Status.Accepted;
            await _crimeRepo.Save();

            var eventMessage = _mapper.Map<CrimeUpdateEvent>(crimeToUpdate);
            await _publishEndpoint.Publish(eventMessage);

            return Ok();
        }
    }
}
