using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TTenantSub
    {
        public TTenantSub()
        {
            TRoles = new HashSet<TRole>();
            TSystemCodeValues = new HashSet<TSystemCodeValue>();
        }

        public Guid TenantSubId { get; set; }
        public Guid TenantMainId { get; set; }
        public string? TenantName { get; set; }
        public string? TelNo { get; set; }
        public string? FaxNo { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public byte[]? Logo { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? TenantCode { get; set; }
        public int MaxActiveUserCount { get; set; }

        public virtual TTenantMain TenantMain { get; set; } = null!;
        public virtual ICollection<TRole> TRoles { get; set; }
        public virtual ICollection<TSystemCodeValue> TSystemCodeValues { get; set; }
    }
}
