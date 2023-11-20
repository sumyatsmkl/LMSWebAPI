using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess.Repositories.Setup
{
    public interface ISystemCodeValueRepository : IRepository<TSystemCodeValue>
    {
        Task<IReadOnlyCollection<TSystemCodeValue>> GetCountries();
        Task<IReadOnlyCollection<TSystemCodeValue>> GetLanguages();
    }
}
