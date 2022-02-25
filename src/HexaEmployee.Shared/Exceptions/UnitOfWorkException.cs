using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace HexaEmployee.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class UnitOfWorkException : Exception
    {
        public UnitOfWorkException()
        {
        }

        public UnitOfWorkException(string message)
            : base(message)
        {
        }

        public UnitOfWorkException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UnitOfWorkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
