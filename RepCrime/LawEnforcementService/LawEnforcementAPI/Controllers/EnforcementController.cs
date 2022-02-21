﻿using AutoMapper;
using LawEnforcement.Application.Contracts;
using LawEnforcement.Application.Exceptions;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<EnforcementReadDto>> GetEnforcementById(string id)
        {
            var enforcement = await _repository.GetByIdAsync(id);

            if (enforcement == null)
                throw new NotFoundException("---> Error: No such id in the database");

            return Ok(_mapper.Map<EnforcementReadDto>(enforcement));
        }

    }
}
