using DataAccess.DTO;
using DataAccess.Repositories.Profile;
using DataAccess.Repositories.Setup;
using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Response;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class SystemCodeValueService:ISystemCodeValueService
    {
        private readonly ISystemCodeValueRepository _setupRepository;

        public SystemCodeValueService(ISystemCodeValueRepository setupRepository)
        {
            _setupRepository = setupRepository;
        }

        public async Task<TSystemCodeValue> GetSystemCodeValueById(Guid codeValueId)
        {
            return await _setupRepository.Get(codeValueId);
        }

        public async Task<WebResponseContent> GetAllByCodeTypeId(int codeTypeId)
        {
            return await _setupRepository.GetAllByCodeTypeId(codeTypeId);
        }     

        public async Task<TSystemCodeValue> UpdateSetup(TSystemCodeValue setupObj)
        {
            TSystemCodeValue setupResult = await _setupRepository.Find(setupObj.CodeValueId);
            return await _setupRepository.Update(setupResult);
        }
    }
}
