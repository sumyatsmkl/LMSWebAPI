using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TProfileAccount
    {
        public Guid ProfileAccountId { get; set; }
        public Guid TenantSubId { get; set; }
        public Guid ProfileId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AccountStatus { get; set; }
        public bool? IsMustChangePassword { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? LoginRetryCount { get; set; }
        public DateTime? ReactivationDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual TProfile TProfile { get; set; } = null!;
    }
}
