using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TProfileRole
    {
        public Guid ProfileRoleId { get; set; }
        public Guid TenantSubId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid RoleId { get; set; }
        public int Status { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual TRole TRole { get; set; } = null!;
    }
}
