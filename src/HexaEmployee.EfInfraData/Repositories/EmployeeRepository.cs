using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.EfInfraData.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HexaEmployee.EfInfraData.Repositories
{
    public class EmployeeRepository : IEmployees
    {
        private readonly HexaEmployeeDbContext _context;

        public EmployeeRepository(HexaEmployeeDbContext context) =>
            _context = context;

        public async Task<EmployeeEntity> GetById(Guid id) =>
            await _context.Employees
                .FirstOrDefaultAsync(employee => employee.Id.Equals(id));
    }
}
