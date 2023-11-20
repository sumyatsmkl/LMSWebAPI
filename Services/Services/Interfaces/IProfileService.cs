using Domain.Entities;
using Domain.Enums;
using Domain.Utilities.Request;
using Domain.Utilities.Response;

namespace Services.Services.Interfaces
{
    public interface IProfileService
    {
        Task<WebResponseContent> Login(LoginInfo loginInfo);

        Task<WebResponseContent> Register(RegisterRequest loginInfo);
    }
}
