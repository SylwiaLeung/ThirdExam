using AutoMapper;
using CommonItems.Dtos;
using CommonItems.Exceptions;
using CommonItems.Models;
using EventBus.Messaging.Events;
using LawEnforcement.Application.Contracts;
using LawEnforcement.Application.HttpClients;
using LawEnforcement.Domain.DTO;
using LawEnforcement.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly ICrimeEventClient _httpClient;

        public EnforcementController(IEnforcementRepository repository,
            ICrimeRepository crimeRepo,
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            ICrimeEventClient httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _crimeRepo = crimeRepo;
            _publishEndpoint = publishEndpoint;
            _httpClient = httpClient;
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

        [HttpGet("unitstats")]
        public async Task<ActionResult<IEnumerable<EnforcementStatsReadDto>>> GetCrimesPerEnforcementUnit()
        {
            var enforcement = await _repository.GetAllAsync();

            return Ok(_mapper.Map<List<EnforcementStatsReadDto>>(enforcement));
        }

        [HttpPost]
        public async Task<ActionResult<EnforcementReadDto>> CreateEnforcementUnit(EnforcementCreateDto enforcement)
        {
            var enforcementModel = _mapper.Map<Enforcement>(enforcement);

            await _repository.AddAsync(enforcementModel);
            await _repository.Save();

            return Ok(_mapper.Map<EnforcementReadDto>(enforcementModel));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateEnforcementUnit(string id, JsonPatchDocument<CrimeUpdateDto> patchDoc)
        {
            var crimeModel = await _crimeRepo.GetByIdAsync(id);

            if (crimeModel == null)
                throw new NotFoundException("No such id in the database");

            var crimeToPatch = _mapper.Map<CrimeUpdateDto>(crimeModel);

            patchDoc.ApplyTo(crimeToPatch, ModelState);
            if (!TryValidateModel(crimeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(crimeToPatch, crimeModel);
            await _crimeRepo.Save();

            var eventMessage = _mapper.Map<CrimeUpdateEvent>(crimeModel);
            await _publishEndpoint.Publish(eventMessage);
            await _httpClient.SendUpdatedCrime(crimeModel);

            return NoContent();
        }
    }
}
