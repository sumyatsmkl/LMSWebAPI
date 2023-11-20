using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using AutoMapper;
using DataAccess.DTO;

namespace WebAPI.Controllers
{
    [Route("api/setup")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly ISystemCodeValueService _setupService;
        private readonly IMapper _mapper;

        public SetupController(ISystemCodeValueService setupService, IMapper mapper)
        {
            _setupService = setupService;
            _mapper = mapper;
        }

        [HttpGet("getCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SystemCodeValueDTO>>> GetCountries()
        {
            var countries = await _setupService.GetCountries();
            return Ok(_mapper.Map<List<SystemCodeValueDTO>>(countries));
        }

        [HttpGet("getLanguages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SystemCodeValueDTO>>> GetLanguages()
        {
            var languages = await _setupService.GetLanguages();            
            return Ok(_mapper.Map<List<SystemCodeValueDTO>>(languages));
        }
    }
}
