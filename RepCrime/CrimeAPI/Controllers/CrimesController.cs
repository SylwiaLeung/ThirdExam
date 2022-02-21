using AutoMapper;
using CrimeService.Models;
using CrimeService.Models.Dtos;
using CrimeService.Services.Repositories;
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
        public async Task<ActionResult<IEnumerable<CrimeReadDto>>> GetCrimes()
        {
            var crimes = await _repository.GetCrimes();

            return Ok(_mapper.Map<List<CrimeReadDto>>(crimes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CrimeReadDto>> GetCrimeById(string id)
        {
            var crime = await _repository.GetCrimeById(id);

            return Ok(_mapper.Map<CrimeReadDto>(crime));
        }

        [HttpGet]
        public async Task<ActionResult<CrimeReadDto>> AddCrime(CrimeCreateDto crimeDto)
        {
            var crimeModel = _mapper.Map<Crime>(crimeDto);

            await _repository.AddCrime(crimeModel);

            _logger.LogInformation("---> Added crime to the databse");

            var eventMessage = _mapper.Map<CrimeEvent>(crimeModel);
            await _publishEndpoint.Publish(eventMessage);

            return Ok(_mapper.Map<CrimeReadDto>(crimeModel));
        }
    }
}
