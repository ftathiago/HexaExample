using HexaEmployee.Api.Models.Requests;
using HexaEmployee.Api.Models.Responses;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HexaEmployee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _service;
        private readonly ISampleRepository _repository;

        public SampleController(ISampleService service, ISampleRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        /// <summary>Describe briefly what your endpoint does.</summary>
        /// <remarks>Here you can add a full description of what your endpoint does.</remarks>
        /// <param name="id" example="1">You can comment your param, saying what its purpose is.</param>
        /// <response code="200">Specify all http status codes your endpoint should use.</response>
        /// <response code="404">And please, check out the status code RFC.</response>
        /// <response code="500">You can specify the most probable reason why a Server error may occurs.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var sample = _service.GetSampleBy(id.Value);

            if (sample is null)
            {
                return NotFound();
            }

            return Ok(SampleResponse.From(sample));
        }

        /// <summary>This endpoint is just to show how to work with objects at querystring.</summary>
        /// <param name="queryString">I recommend you do not disable SA1611, because it helps you!</param>
        /// <param name="page">Paging.</param>
        /// <response code="200">Specify all http status codes your endpoint should use.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetObjectFromQuery(
            [FromQuery] QueryStringTest queryString,
            [FromQuery] PaginationRequest page)
        {
            return Ok(new
            {
                queryString,
                page,
            });
        }
    }
}
