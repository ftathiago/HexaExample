using System.Threading.Tasks;

namespace HexaEmployee.WarmUp.Abstractions
{
    public interface IWarmUpCommand
    {
        Task Execute();
    }
}
