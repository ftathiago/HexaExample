using HexaEmployee.Api.Models.Requests;
using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.Domain.Services;
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
        private readonly IEmployee _employees;
        private readonly IEmployeeService _service;

        public EmployeesController(
            IEmployee employees,
            IEmployeeService service)
        {
            _employees = employees;
            _service = service;
        }

        /// <summary>Returns employees data.</summary>
        /// <remarks>Returns basic employee data, and current salary and it's raising date.</remarks>
        /// <param name="employeeId" example="532f8988-3319-4e3c-b280-671562beea58">Employee key.</param>
        /// <response code="200">Employee was found.</response>
        /// <response code="404">There is no employee with provided key.</response>
        /// <response code="500">Server error.</response>
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{employeeId:guid}")]
        public async Task<IActionResult> GetById(Guid employeeId) =>
            Ok(await _employees.GetById(employeeId));

        /// <summary>Update employee salary.</summary>
        /// <remarks>A possible way to call a REST/RPC is putting, after a colon, the verb
        /// that describe the intent. But Swashbuckle will only support this at version 6.3.0.
        /// Because of this, for now, we are enabling this endpoint.
        /// [HttpPost("{id:guid}:raise-salary")]
        /// </remarks>
        /// <param name="employeeId" example="532f8988-3319-4e3c-b280-671562beea58">Employee key.</param>
        /// <param name="salaryRaising">Salary rainsing data.</param>
        /// <response code="200">Employee was found.</response>
        /// <response code="404">There is no employee with provided key.</response>
        /// <response code="500">Server error.</response>
        [ProducesResponseType(typeof(EmployeeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{employeeId}/raise-salary")]
        public async Task<IActionResult> RaiseSalary(
            [FromRoute] Guid employeeId,
            [FromBody] SalaryRaisingRequest salaryRaising)
        {
            var raisingData = (salaryRaising.NewSalaryRaisedAt, salaryRaising.NewSalary);
            await _service.UpdateSalaryAsync(raisingData, employeeId);
            return Ok();
        }
    }
}
