using System;
using System.Runtime.Serialization;

namespace HexaEmployee.Domain.Exceptions
{
    [Serializable]
    public class InvalidSalaryDateException : Exception
    {
        private const string ErrorMessage =
            "The new salary date ({0}) must be after {1}.";

        public InvalidSalaryDateException(DateTimeOffset currentRaiseDate, DateTimeOffset newRaiseDate)
            : base(string.Format(ErrorMessage, newRaiseDate, currentRaiseDate))
        {
        }

        protected InvalidSalaryDateException()
        {
        }

        protected InvalidSalaryDateException(string message)
            : base(message)
        {
        }

        protected InvalidSalaryDateException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidSalaryDateException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
