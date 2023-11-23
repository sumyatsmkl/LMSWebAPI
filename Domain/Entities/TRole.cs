using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TRole
    {
        public TRole()
        {
            TProfileRoles = new HashSet<TProfileRole>();
        }

        public Guid RoleId { get; set; }
        public Guid TenantSubId { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public int Status { get; set; }
        public string? Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public bool? AllowSelfRegister { get; set; }

        public virtual TTenantSub TenantSub { get; set; } = null!;
        public virtual ICollection<TProfileRole> TProfileRoles { get; set; }
    }
}
