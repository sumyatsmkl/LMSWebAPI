using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Request;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using AutoMapper;
using DataAccess.DTO;
using Domain.Utilities.Response;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpPost,Route("login")]
        public async Task<WebResponseContent> Login([FromBody] LoginInfo loginInfo)
        {
            return await _profileService.Login(loginInfo);
        }

        [HttpPost, Route("register")]
        public async Task<WebResponseContent> Register([FromBody] RegisterRequest registerData)
        {
            return await _profileService.Register(registerData);
        }


    }
}
