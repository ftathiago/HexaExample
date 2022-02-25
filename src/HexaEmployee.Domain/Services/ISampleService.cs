using HexaEmployee.Domain.Entities;

namespace HexaEmployee.Domain.Services
{
    public interface ISampleService
    {
        SampleEntity GetSampleBy(int id);
    }
}
