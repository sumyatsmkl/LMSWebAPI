using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TSystemCodeValue
    {
        public Guid CodeValueId { get; set; }
        public string? Code { get; set; }
        public string CodeValue { get; set; } = null!;
        public int CodeTypeId { get; set; }
        public int Status { get; set; }
        public bool? IsDefault { get; set; }
        public string? Remarks { get; set; }
        public int? CodeOrder { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid? TenantSubId { get; set; }

        public virtual TTenantSub? TenantSub { get; set; }
    }
}
