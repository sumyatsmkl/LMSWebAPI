using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TProfile
    {
        public TProfile()
        {
            TProfileAccounts = new HashSet<TProfileAccount>();
            TProfileEducations = new HashSet<TProfileEducation>();
        }

        public Guid ProfileId { get; set; }
        public Guid TenantSubId { get; set; }
        public Guid? SalutationTitleId { get; set; }
        public string FullName { get; set; } = null!;
        public string? PhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Idno { get; set; } = null!;
        public Guid IdtypeId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? MaritalStatusId { get; set; }
        public int ProfileStatus { get; set; }
        public string? Remarks { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual ICollection<TProfileAccount> TProfileAccounts { get; set; }
        public virtual ICollection<TProfileEducation> TProfileEducations { get; set; }
    }
}
