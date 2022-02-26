using HexaEmployee.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace HexaEmployee.Domain.Repositories
{
    public interface IEmployee
    {
        Task<EmployeeEntity> GetById(Guid id);
    }
}
