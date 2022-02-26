using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HexaEmployee.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class EmployeesController : Controller
    {
        private readonly IEmployees _employees;

        public EmployeesController(IEmployees employees) =>
            _employees = employees;

        /// <summary>Returns employees data.</summary>
        /// <remarks>Returns basic employee data, and current salary and it's raising date.</remarks>
        /// <param name="id" example="532f8988-3319-4e3c-b280-671562beea58">Employee key.</param>
        /// <response code="200">Employee was found.</response>
        /// <response code="404">There is no employee with provided key.</response>
        /// <response code="500">Server error.</response>
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _employees.GetById(id));
    }
}
