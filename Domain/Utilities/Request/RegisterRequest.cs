using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilities.Request
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Guid IdType { get; set; }
        public string IdNo { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

    }
}
