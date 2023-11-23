using DataAccess.Repositories.Profile;
using DataAccess.Repositories.Role;
using DataAccess.Repositories.Setup;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Response;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<WebResponseContent> GetAllowSelfRegisterRoles()
        {
            return await _roleRepository.GetAllowSelfRegisterRoles();
        }
        public async Task<TRole> UpdateRole(TRole role)
        {
            TRole roleResult = await _roleRepository.Find(role.RoleId);
            return await _roleRepository.Update(roleResult);
        }

        public async Task<IReadOnlyCollection<TRole>> GetAllRoles()
        {
            return await _roleRepository.GetAll();
        }
    }
}
