using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Enums;
using System.Data;
using System.Net;
using Domain.Utilities.Response;

namespace DataAccess.Repositories.Setup
{
    public class SystemCodeValueRepository:ISystemCodeValueRepository
    {
        private readonly LMSDBContext _dbContext;
        public SystemCodeValueRepository(LMSDBContext context) => _dbContext = context;
        WebResponseContent webResponse = new WebResponseContent();
        public async Task<IReadOnlyCollection<TSystemCodeValue>> GetAll()
        {
            return await _dbContext.TSystemCodeValues.ToListAsync();
        }    

        public async Task<WebResponseContent> GetAllByCodeTypeId(int codeTypeId)
        {
            var setups= await _dbContext.TSystemCodeValues.Where(x => x.CodeTypeId == codeTypeId).ToListAsync();
            webResponse.Data = setups;
            return webResponse.OK(ResponseType.RetrieveSuccess);
        }
        public async Task<TSystemCodeValue> Get(Guid codeValueId)
        {
            return await Find(codeValueId) ?? throw new Exception($"Does not exist.");
        }
        public async Task<TSystemCodeValue> Find(Guid codeValueId)
        {
            return await _dbContext.TSystemCodeValues
             
                .SingleOrDefaultAsync(systemCodeValue => systemCodeValue.CodeValueId == codeValueId);
        }

        private async Task<bool> IsExistAsync(Guid codeValueId)
        {
            return await _dbContext.TSystemCodeValues.AnyAsync(systemCodeValue => systemCodeValue.CodeValueId == codeValueId);
        }

        private async Task<bool> IsCodeValueExist(String codeValue, int codeTypeId)
        {
            return await _dbContext.TSystemCodeValues.AnyAsync(systemCodeValue => systemCodeValue.CodeValue == codeValue && systemCodeValue.CodeTypeId == codeTypeId);
        }
        public async Task<TSystemCodeValue> Create(TSystemCodeValue item)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeInserting");

                if (IsCodeValueExist(item.CodeValue, item.CodeTypeId).Result)
                    throw new Exception($"{item.CodeValue} is already exist");

                EntityEntry<TSystemCodeValue> newSetup = await _dbContext.TSystemCodeValues.AddAsync(item);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return newSetup.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeInserting");
                throw;
            }
        }
        public async Task<TSystemCodeValue> Update(TSystemCodeValue systemCodeValue)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeUpdating");

                if (!IsExistAsync(systemCodeValue.CodeValueId).Result)
                    throw new Exception($"The item that you want to update doesn't exist");

                EntityEntry<TSystemCodeValue> updatedItem = _dbContext.TSystemCodeValues.Update(systemCodeValue);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return updatedItem.Entity;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeUpdating");
                throw;
            }
        }

        public async Task<TSystemCodeValue> Delete(Guid codeValueId)
        {
            await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("BeforeRemoved");

                TSystemCodeValue itemToRemove = await Get(codeValueId);

                _dbContext.TSystemCodeValues.Remove(itemToRemove);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return itemToRemove;
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("BeforeRemoved");
                throw;
            }
        }
    }
}
