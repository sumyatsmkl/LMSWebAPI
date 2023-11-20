using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TTenantMain
    {
        public TTenantMain()
        {
            TTenantSubs = new HashSet<TTenantSub>();
        }

        public Guid TenantMainId { get; set; }
        public string? TenantMainName { get; set; }
        public string? Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual ICollection<TTenantSub> TTenantSubs { get; set; }
    }
}
