using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Utilities.Response;

namespace DataAccess.Repositories.Setup
{
    public interface ISystemCodeValueRepository : IRepository<TSystemCodeValue>
    {       
        Task<WebResponseContent> GetAllByCodeTypeId(int codeTypeId);
    }
}
