using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public record RoleDTO
    {
        public Guid RoleId { get; init; }
        public string RoleName { get; init; } = null!;
    }
}
