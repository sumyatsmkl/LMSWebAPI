using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Tools
{
    public class CustomException:Exception
    {
        public CustomException() { }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static void ThrowIfNullOrWhiteSpace(string argument)
        {
            if (string.IsNullOrWhiteSpace(argument)) Throw(argument);
        }

        [DoesNotReturn]
        private static void Throw(string paramName) => throw new ArgumentNullException(paramName);
    }
}
