using Bogus;
using FluentAssertions;
using HexaEmployee.Api.Models.Requests;
using HexaEmployee.Api.Tests.Fixtures;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace HexaEmployee.Api.Tests.Models.Requests
{
    public class PaginationRequestTest
    {
        private readonly Faker _faker = Fixture.Get();

        [Fact]
        public void Should_BeValid_When_RangesAreOk()
        {
            // Given
            var pagination = new PaginationRequest()
            {
                {
                    "offset", _faker.Random.Number(
                        min: PaginationRequest.MinOffset,
                        max: int.MaxValue).ToString()
                },
                {
                    "limit", _faker.Random.Number(
                        min: PaginationRequest.MinRecordsPerPage,
                        max: PaginationRequest.MaxRecordsPerPage).ToString()
                },
            };
            var context = new ValidationContext(pagination);

            // When
            var validation = pagination.Validate(context);

            // Then
            validation.Should().BeEmpty();
        }

        [Fact]
        public void Should_BeInvalid_When_OffsetIsLessThanOne()
        {
            // Given
            const string ExpectedMessage = "An offset must be greather or equal than 1";
            var pagination = new PaginationRequest()
            {
                { "offset", "0" },
            };
            var context = new ValidationContext(pagination);

            // When
            var validation = pagination.Validate(context);

            // Then
            validation.Should().HaveCount(1);
            validation.Should().Contain(x => x.ErrorMessage == ExpectedMessage);
        }

        [Fact]
        public void Should_BeInvalid_When_ViolatesMaxRecordsPerPage()
        {
            // Given
            var expectedMessage =
                $"A limit must be between {PaginationRequest.MinRecordsPerPage} and {PaginationRequest.MaxRecordsPerPage}.";
            var pagination = new PaginationRequest()
            {
                {
                    "limit", _faker.Random.Number(
                        min: PaginationRequest.MaxRecordsPerPage + 1,
                        max: int.MaxValue).ToString()
                },
            };
            var context = new ValidationContext(pagination);

            // When
            var validation = pagination.Validate(context);

            // Then
            validation.Should().HaveCount(1);
            validation.Should().Contain(x => x.ErrorMessage == expectedMessage);
        }
    }
}
