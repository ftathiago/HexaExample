using System;
using System.Runtime.Serialization;

namespace HexaEmployee.Domain.Exceptions
{
    [Serializable]
    public class InvalidSalaryValueException : Exception
    {
        private const string ErrorMessage =
            "The new salary ({0}) can not be less than current salary ({1}).";

        public InvalidSalaryValueException(decimal currentValue, decimal newValue)
            : base(string.Format(ErrorMessage, currentValue, newValue))
        {
        }

        protected InvalidSalaryValueException()
        {
        }

        protected InvalidSalaryValueException(string message)
            : base(message)
        {
        }

        protected InvalidSalaryValueException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidSalaryValueException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
