using Bogus;
using FluentAssertions;
using HexaEmployee.Api.Controllers;
using HexaEmployee.Api.Models.Requests;
using HexaEmployee.Api.Tests.Fixtures;
using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.Domain.Services;
using HexaEmployee.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using Xunit;

namespace HexaEmployee.Api.Tests.Controllers
{
    public class SampleControllerTests
    {
        private readonly Faker _faker = Fixture.Get();
        private readonly Mock<ISampleService> _service;
        private readonly Mock<ISampleRepository> _repository;

        public SampleControllerTests()
        {
            _service = new Mock<ISampleService>(MockBehavior.Strict);
            _repository = new Mock<ISampleRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void Should_ReturnOkResultWithSampleResponse()
        {
            // Given
            var mockSampleTable = GetSampleEntity();
            _service
                .Setup(s => s.GetSampleBy(mockSampleTable.Id))
                .Returns(mockSampleTable);
            var controller = new SampleController(
                _service.Object,
                _repository.Object);

            // When
            var response = controller.Get(mockSampleTable.Id);
            var result = response as OkObjectResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK.AsInteger());
        }

        [Fact]
        public void Should_ReturnBadRequest_When_IdIsNull()
        {
            // Given
            var controller = new SampleController(
                _service.Object,
                _repository.Object);

            // When
            var response = controller.Get(null);
            var result = response as BadRequestResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest.AsInteger());
        }

        [Fact]
        public void Should_ReturnNotFound_When_SampleDoesNotExists()
        {
            // Given
            var notExistId = Fixture.Get().Random.Int();
            _service
                .Setup(s => s.GetSampleBy(It.IsAny<int>()))
                .Returns(null as SampleEntity);
            var controller = new SampleController(
                _service.Object,
                _repository.Object);

            // When
            var response = controller.Get(notExistId);
            var result = response as NotFoundResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound.AsInteger());
        }

        [Fact]
        public void Should_EchoQueryParams()
        {
            // Given
            var queryString = new QueryStringTest
            {
                Id = Guid.NewGuid(),
                QueryNumber = _faker.Random.Int(),
                RequiredField = _faker.Lorem.Sentences(),
            };
            var page = new PaginationRequest
            {
                { "offset", _faker.Random.Int(PaginationRequest.MinOffset).ToString() },
                { "limit", _faker.Random.Int(PaginationRequest.MaxRecordsPerPage).ToString() },
            };
            var controller = new SampleController(
                _service.Object,
                _repository.Object);

            // When
            var result = controller.GetObjectFromQuery(queryString, page);

            // Then
            result.Should().BeOfType<OkObjectResult>();
            var objectResponse = (result as OkObjectResult)?.Value;
            objectResponse.Should().BeEquivalentTo(new
            {
                queryString,
                page,
            });
        }

        private static SampleEntity GetSampleEntity() => new()
        {
            Id = Fixture.Get().Random.Int(),
            TestProperty = Fixture.Get().Lorem.Sentence(),
            Active = Fixture.Get().Random.Bool(),
        };
    }
}
