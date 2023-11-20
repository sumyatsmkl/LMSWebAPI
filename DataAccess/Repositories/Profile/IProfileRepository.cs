using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Utilities.Request;
using Domain.Utilities.Response;

namespace DataAccess.Repositories.Profile
{
    public interface IProfileRepository:IRepository<TProfile>
    {
        Task<WebResponseContent> Login(LoginInfo loginInfo);

        Task<WebResponseContent> Register(RegisterRequest registerData);
    }
}
