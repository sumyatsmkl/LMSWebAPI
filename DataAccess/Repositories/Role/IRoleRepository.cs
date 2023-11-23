using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Utilities.Request;
using Domain.Utilities.Response;
namespace DataAccess.Repositories.Role
{
    public interface IRoleRepository : IRepository<TRole>
    {
        Task<WebResponseContent> GetAllowSelfRegisterRoles();
    }
}
