using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ResponseType
    {
        ServerError = 1,
        LoginExpiration = 302,
        ParametersLack = 303,
        TokenExpiration,
        PINError,
        NoPermissions,
        NoRolePermissions,
        LoginError,
        AccountLocked,
        LoginSuccess,
        SaveSuccess,
        AuditSuccess,
        OperSuccess,
        RegisterSuccess,
        ModifyPwdSuccess,
        EidtSuccess,
        DelSuccess,
        NoKey,
        NoKeyDel,
        KeyError,
        Other
    }

    public enum ResponseCode
    {
        Register_DuplicateUserName = 1001,
        Register_DuplicateEmail = 1002,
        Register_DuplicateIDNo = 1003,
        Register_InvalidPasswordStrength = 1004,
        Register_Success = 1005,

        Login_UserNameNotFound = 1006,
        Login_IncorrectPassword = 1007,
        Login_Success = 1008
    }
}
