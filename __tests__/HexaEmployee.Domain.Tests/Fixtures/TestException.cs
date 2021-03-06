using System;
using System.Runtime.Serialization;

namespace HexaEmployee.Domain.Tests.Fixtures
{
    [Serializable]
    public class TestException : Exception
    {
        public TestException()
            : base(Fixture.Get().Lorem.Sentence())
        {
        }

        public TestException(string message)
            : base(message)
        {
        }

        public TestException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        protected TestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
