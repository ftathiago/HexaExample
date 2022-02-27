using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.EfInfraData.Contexts;
using HexaEmployee.EfInfraData.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HexaEmployee.EfInfraData.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly HexaEmployeeDbContext _context;

        public EmployeeRepository(HexaEmployeeDbContext context) =>
            _context = context;

        public async Task<EmployeeEntity> GetById(Guid id)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(employee => employee.Id.Equals(id));

            var lastSalary = await _context.SalaryHistory
                .AsNoTracking()
                .Where(history => history.EmployeeId.Equals(id))
                .OrderByDescending(history => history.SalaryRaisedAt)
                .Take(1)
                .SingleOrDefaultAsync();

            return employee.ToEntity(lastSalary);
        }
    }
}
