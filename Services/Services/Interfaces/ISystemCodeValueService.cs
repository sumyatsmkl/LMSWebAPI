using Domain.Entities;
using Domain.Enums;

namespace Services.Services.Interfaces
{
    public interface ISystemCodeValueService
    {
        Task<TSystemCodeValue> GetSystemCodeValueById(Guid codeValueId);
        Task<IReadOnlyCollection<TSystemCodeValue>> GetCountries();
        Task<IReadOnlyCollection<TSystemCodeValue>> GetLanguages();

        Task<TSystemCodeValue> UpdateSetup(TSystemCodeValue item);

    }
}
