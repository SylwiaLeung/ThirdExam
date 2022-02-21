using AutoMapper;
using CrimeService.Models.Dtos;
using CrimeService.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrimeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimesController : ControllerBase
    {
        private readonly ICrimeRepository _repository;
        private readonly IMapper _mapper;

        public CrimesController(ICrimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
