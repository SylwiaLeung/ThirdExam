using AutoMapper;
using LawEnforcement.Application.Contracts;
using LawEnforcement.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LawEnforcementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnforcementController : ControllerBase
    {
        private readonly IEnforcementRepository _repository;
        private readonly ILogger<EnforcementController> _logger;
        private readonly IMapper _mapper;

        public EnforcementController(IEnforcementRepository repository, ILogger<EnforcementController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnforcementReadDto>>> GetAllEnforcement()
        {
            var enforcement = await _repository.GetAllAsync();

            return Ok(_mapper.Map<List<EnforcementReadDto>>(enforcement));
        }
       
    }
}
