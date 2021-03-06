using FluentAssertions;
using HexaEmployee.Shared.Extensions;
using HexaEmployee.Shared.Tests.Fixtures;
using System;
using Xunit;

namespace HexaEmployee.Shared.Tests.Extensions
{
    public class ExceptionExtensionTest
    {
        [Fact]
        public void ShouldGetMessageFromException()
        {
            // Given
            var exception = new Exception(
                Fixture.Get().Lorem.Sentence());

            // When
            var message = exception.GetAllMessage(Environment.NewLine);

            // Then
            message.Should().Contain(exception.Message);
        }

        [Fact]
        public void ShouldGetMessagesFromInnerException()
        {
            // Given
            var exception = new Exception(
                Fixture.Get().Lorem.Sentence(),
                new Exception(Fixture.Get().Lorem.Sentence()));

            // When
            var message = exception.GetAllMessage(Environment.NewLine);

            // Then
            message.Should().Contain(exception.Message);
            message.Should().Contain(exception.InnerException.Message);
        }

        [Fact]
        public void TestName()
        {
            // Given
            var exceptionOne = new Exception(Fixture.Get().Lorem.Sentence());
            var exceptionTwo = new Exception(Fixture.Get().Lorem.Sentence());
            var exception = new AggregateException(
                exceptionOne,
                exceptionTwo);

            // When
            var message = exception.GetAllMessage(Environment.NewLine);

            // Then
            message.Should().Contain(exceptionOne.Message);
            message.Should().Contain(exceptionTwo.Message);
        }
    }
}
