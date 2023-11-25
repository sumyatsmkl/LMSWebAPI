using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class LoginDTO
    {
        public Guid ProfileAccountId { get; init; }
        public Guid ProfileId { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string AccessToken { get; init; }
        public string Email { get; init; }        
        public int AccountStatus { get; init; }
        public bool? IsMustChangePassword { get; init; }
        public DateTime? LastLoginTime { get; init; }
        public int? LoginRetryCount { get; init; }
        public DateTime? ReactivationDate { get; init; }
    }
}
