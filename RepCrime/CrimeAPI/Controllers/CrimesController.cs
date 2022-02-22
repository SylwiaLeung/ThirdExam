using AutoMapper;
using CommonItems.Exceptions;
using CommonItems.Models;
using CrimeService.Models;
using CrimeService.Models.Dtos;
using CrimeService.Services.Repositories;
using EventBus.Messaging.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CrimeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimesController : ControllerBase
    {
        private readonly ICrimeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CrimesController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public CrimesController(ICrimeRepository repository, IMapper mapper, ILogger<CrimesController> logger, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrimeReadDto>>> GetCrimes([FromQuery] QueryModel query)
        {


            if (query.PageNumber == 0 || query.PageSize == 0)
                throw new BadRequestException("You must specify page number and page size.");

            var crimes = await _repository.GetCrimes();
            var crimeDtos = _mapper.Map<List<CrimeReadDto>>(crimes);

            var baseQuery = crimeDtos
                .Where(e => query.SearchPhrase == null
                || e.CrimeType.ToString().ToLower().Contains(query.SearchPhrase.ToLower())
                || e.PlaceOfCrime.ToLower().Contains(query.SearchPhrase.ToLower()));

            var filteredDtos = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var result = new PagedResultModel<CrimeReadDto>(filteredDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CrimeReadDto>> GetCrimeById(string id)
        {
            var crime = await _repository.GetCrimeById(id);

            return Ok(_mapper.Map<CrimeReadDto>(crime));
        }

        [HttpPost]
        public async Task<ActionResult<CrimeReadDto>> AddCrime(CrimeCreateDto crimeDto)
        {
            var crimeModel = _mapper.Map<Crime>(crimeDto);

            await _repository.AddCrime(crimeModel);

            _logger.LogInformation("---> Added crime to the databse");

            var eventMessage = _mapper.Map<CrimeEvent>(crimeModel);
            await _publishEndpoint.Publish(eventMessage);

            return Ok(_mapper.Map<CrimeReadDto>(crimeModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CrimeReadDto>> UpdateCrime(string id, CrimeEvent crime)
        {
            _logger.LogInformation("---> Invoking PUT CRIME method");

            var crimeToUpdate = await _repository.GetCrimeById(id);

            if (crimeToUpdate == null)
                throw new NotFoundException("No such ID in the database");

            var updatedCrime = _mapper.Map(crime, crimeToUpdate);
            var success = await _repository.UpdateCrime(updatedCrime);

            if (!success)
                throw new Exception("Could not update the crime");

            return Ok(_mapper.Map<CrimeReadDto>(updatedCrime));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCrime(string id)
        {
            var crimeToDelete = await _repository.GetCrimeById(id);

            if (crimeToDelete == null)
                throw new NotFoundException("No crime with this ID in the database");

            var success = await _repository.DeleteCrime(id);

            if (!success)
                throw new Exception("Could not delete the crime");

            return NoContent();
        }
    }
}
