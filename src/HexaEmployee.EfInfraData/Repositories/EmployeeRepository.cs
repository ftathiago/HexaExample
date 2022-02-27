using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.EfInfraData.Contexts;
using HexaEmployee.EfInfraData.Extensions;
using HexaEmployee.EfInfraData.Models;
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

            var lastSalary = await GetLastSalaryFrom(id);

            return employee.ToEntity(lastSalary);
        }

        public async Task UpdateAsync(EmployeeEntity entity)
        {
            var (employee, salaryHistory) = entity.ToTable();

            _context.Entry(employee).State = EntityState.Modified;
            _context.Employees.Update(employee);
            await _context.SalaryHistory.AddAsync(salaryHistory);
            await _context.SaveChangesAsync();
        }

        private async Task<SalaryHistoryTable> GetLastSalaryFrom(Guid employeeId) =>
            await _context.SalaryHistory
                .AsNoTracking()
                .Where(history => history.EmployeeId.Equals(employeeId))
                .OrderByDescending(history => history.SalaryRaisedAt)
                .Take(1)
                .SingleOrDefaultAsync();
    }
}
