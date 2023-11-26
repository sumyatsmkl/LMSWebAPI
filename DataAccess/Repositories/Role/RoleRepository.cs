using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Tools;
using Domain.Utilities.Helpers;
using Domain.Utilities.Request;
using Domain.Utilities.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace DataAccess.Repositories.Role
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LMSDBContext _dbContext;
        public RoleRepository(LMSDBContext context) => _dbContext = context;
        WebResponseContent webResponse = new WebResponseContent();
        public async Task<IReadOnlyCollection<TRole>> GetAll()
        {
            return await _dbContext.TRoles.ToListAsync();         
        }

        public async Task<WebResponseContent> GetAllRoles()
        {
            var roles = await _dbContext.TRoles.ToListAsync();
            webResponse.Data = roles;
            return webResponse.OK(ResponseType.RetrieveSuccess);
        }
        public async Task<WebResponseContent> GetAllowSelfRegisterRoles()
        {
            var roles = await _dbContext.TRoles.Where(x => x.AllowSelfRegister == true && x.Status == (int)CommonStatus.Active).ToListAsync();
            webResponse.Data = roles;
            return  webResponse.OK(ResponseType.RetrieveSuccess);
        }
        public async Task<TRole> Get(Guid roleId)
        {
            return await Find(roleId) ?? throw new Exception($"Role does not exist.");
        }
        public async Task<TRole> Find(Guid roleId)
        {
            return await _dbContext.TRoles               
                .SingleOrDefaultAsync(role => role.RoleId == roleId);
        }

        private async Task<bool> IsRoleExistAsync(Guid roleId)
        {
            return await _dbContext.TRoles.AnyAsync(role => role.RoleId == roleId);
        }

        public async Task<TRole> Update(TRole role)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeUpdating");

                if (!IsRoleExistAsync(role.RoleId).Result)
                    throw new Exception($"Role to update doesn't exist");

                EntityEntry<TRole> updatedRole = _dbContext.TRoles.Update(role);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return updatedRole.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeUpdating");
                throw;
            }
        }

        public async Task<TRole> Delete(Guid roleId)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeRemove");

                TRole roleToRemove = await Get(roleId);

                _dbContext.TRoles.Remove(roleToRemove);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return roleToRemove;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeRemove");
                throw;
            }
        }

    }
}
