using HexaEmployee.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace HexaEmployee.Domain.Repositories
{
    public interface IEmployees
    {
        Task<EmployeeEntity> GetById(Guid id);
    }
}
