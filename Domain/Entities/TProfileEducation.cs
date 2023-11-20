using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TProfileEducation
    {
        public Guid EducationId { get; set; }
        public Guid TenantSubId { get; set; }
        public Guid ProfileId { get; set; }
        public string? Institution { get; set; }
        public Guid? QualificationLevelId { get; set; }
        public string? QualificationName { get; set; }
        public Guid? CountryId { get; set; }
        public DateTime? SchoolStartDate { get; set; }
        public DateTime? SchoolEndDate { get; set; }
        public Guid? LanguageProficiencyId { get; set; }
        public bool? IsHighestQualification { get; set; }
        public DateTime? YearObtained { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsNextHighestQualification { get; set; }

        public virtual TProfile TProfile { get; set; } = null!;
    }
}
