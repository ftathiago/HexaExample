using HexaEmployee.Domain.Entities;
using HexaEmployee.EfInfraData.Models;

namespace HexaEmployee.EfInfraData.Extensions
{
    public static class SampleTableExtension
    {
        public static SampleEntity AsEntity(this SampleTable table) => table is null
            ? default
            : new SampleEntity()
            {
                Id = table.Id,
                TestProperty = table.TestProperty,
                Active = table.Active,
            };

        public static SampleTable AsTable(this SampleEntity entity) => entity is null
            ? default
            : new SampleTable
            {
                Id = entity.Id,
                TestProperty = entity.TestProperty,
                Active = entity.Active,
            };
    }
}
