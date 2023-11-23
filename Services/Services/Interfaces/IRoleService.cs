﻿using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Request;
using Domain.Utilities.Response;

namespace Services.Services.Interfaces
{
    public interface IRoleService
    {
        Task<WebResponseContent> GetAllowSelfRegisterRoles();

        Task<IReadOnlyCollection<TRole>> GetAllRoles();
        Task<TRole> UpdateRole(TRole item);
    }
}
