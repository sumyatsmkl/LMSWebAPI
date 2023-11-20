using DataAccess.Repositories;
using DataAccess.Repositories.Profile;
using DataAccess.Repositories.Setup;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Request;
using Domain.Utilities.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Services.Services.Interfaces;

using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Services.Services
{
    public class ProfileService:IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        WebResponseContent webResponse = new WebResponseContent();
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        
    }
}
