using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Response;

namespace Services.Services.Interfaces
{
    public interface ISystemCodeValueService
    {
        Task<TSystemCodeValue> GetSystemCodeValueById(Guid codeValueId);
        Task<WebResponseContent> GetAllByCodeTypeId(int codeTypeId);
        Task<TSystemCodeValue> UpdateSetup(TSystemCodeValue item);

    }
}
