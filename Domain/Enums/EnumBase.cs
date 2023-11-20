using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum CodeTypeID
    {
        QualificationLevel = 1,
        IDType = 2,
        Gender = 3,
        SalutationTitle =4,
        MaritalStatus =5,
        EmploymentStatus =6,
        Country = 7,
        LanguageProficiency = 8
    }

    public enum ProfileStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum AccountStatus
    {
        Inactive = 0,
        Active = 1,
        Blacklisted = 2,
        Suspended = 3
    }

    public enum QualificationType
    {
        Acadamic = 1,
        Training = 2,
        Professional = 3,
        Management = 4
    }

    public enum CommonStatus
    {
        Inactive = 0,
        Active = 1
    }
}
