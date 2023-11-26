using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using AutoMapper;
using DataAccess.DTO;
using Newtonsoft.Json.Linq;
using System.Net;
using Domain.Utilities.Response;
using Domain.Utilities.Request;
using Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("getAllowRegisterRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<WebResponseContent> GetAllowRegisterRoles()
        {           
            return await _roleService.GetAllowSelfRegisterRoles();
        }

        [Authorize]
        [HttpGet("getAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<WebResponseContent> GetAllRoles()
        {
            return await _roleService.GetAllRoles();            
        }
    }
}
