using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TSystemCurrency
    {
        public Guid CurrencyId { get; set; }
        public Guid TenantSubId { get; set; }
        public string CurrencyName { get; set; } = null!;
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
