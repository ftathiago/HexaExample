using FluentAssertions;
using HexaEmployee.WarmUp.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HexaEmployee.WarmUp.Tests.HealthChecks
{
    public class WarmUpHealthCheckTest
    {
        [Fact]
        public async Task Should_ReturnUnhealthy_When_WarmUpIsNotCompleted()
        {
            // Given
            var warmUpHealthCheck = new WarmUpHealthCheck
            {
                WarmUpCompleted = false,
            };

            // When
            var healthCheckResult = await warmUpHealthCheck.CheckHealthAsync(
                new HealthCheckContext(),
                CancellationToken.None);

            // Then
            healthCheckResult.Should().BeEquivalentTo(new
            {
                Description = "The warmup task is still running.",
                Status = HealthStatus.Unhealthy,
            });
        }

        [Fact]
        public async Task Should_ReturnHealthy_When_WarmUpIsCompleted()
        {
            // Given
            var warmUpHealthCheck = new WarmUpHealthCheck
            {
                WarmUpCompleted = true,
            };

            // When
            var healthCheckResult = await warmUpHealthCheck.CheckHealthAsync(
                new HealthCheckContext(),
                CancellationToken.None);

            // Then
            healthCheckResult.Should().BeEquivalentTo(new
            {
                Description = "The warmup task is finished.",
                Status = HealthStatus.Healthy,
            });
        }
    }
}
