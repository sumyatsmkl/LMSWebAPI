using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Repositories.Profile
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly LMSDBContext _dbContext;
        public ProfileRepository(LMSDBContext context) => _dbContext = context;

        public async Task<IReadOnlyCollection<TProfile>> GetAll()
            => await _dbContext.TProfiles.ToListAsync();

        public async Task<TProfile> Get(Guid profileId)
        {
            return await Find(profileId) ?? throw new Exception($"Profile does not exist.");
        }
        public async Task<TProfile> Find(Guid profileId)
        {
            return await _dbContext.TProfiles
                .Include(profile => profile.TProfileAccounts)
                .Include(profile => profile.TProfileEducations)
                .SingleOrDefaultAsync(profile => profile.ProfileId == profileId);
        }

        private async Task<bool> IsProfileExistAsync(Guid profileId)
        {
            return await _dbContext.TProfiles.AnyAsync(profile => profile.ProfileId == profileId);
        }

        public async Task<TProfile> Update(TProfile profile)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeUpdatingProfile");

                if (!IsProfileExistAsync(profile.ProfileId).Result)
                    throw new Exception($"Profile to update doesn't exist");

                EntityEntry<TProfile> updatedProfile = _dbContext.TProfiles.Update(profile);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return updatedProfile.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeUpdatingProfile");
                throw;
            }
        }

        public async Task<TProfile> Delete(Guid profileId)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeProfileRemoved");

                TProfile profileToRemove = await Get(profileId);

                _dbContext.TProfiles.Remove(profileToRemove);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return profileToRemove;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeProfileRemoved");
                throw;
            }
        }
    }
}
