using System.Threading.Tasks;

namespace CommandsService.Interfaces
{
    public interface IUnitOfWork
    {
        ICommandRepository CommandRepository { get; }
        Task CompleteAsync();
    }
}
