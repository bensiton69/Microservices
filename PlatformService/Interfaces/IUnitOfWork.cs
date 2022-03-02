using System.Threading.Tasks;
using PlatformService.Data;

namespace PlatformService.Interfaces
{
    public interface IUnitOfWork
    {
        IPlatformRepository PlatformRepository { get; }
        Task CompleteAsync();
    }
}