using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using AutoMapper;
using DataAccess.DTO;
using System.Data;
using System.Net;
using Domain.Utilities.Response;

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

        [HttpGet("getAllByCodeTypeId/{codeTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<WebResponseContent> GetAllByCodeTypeId(int codeTypeId)
        {
            return await _setupService.GetAllByCodeTypeId(codeTypeId);
        }
    }
}
