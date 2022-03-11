using System.Threading.Tasks;
using CommandsService.Interfaces;

namespace CommandsService.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICommandRepository CommandRepository { get; }
        private readonly AppDbContext _context;
        public UnitOfWork(ICommandRepository commandRepository, AppDbContext context)
        {
            CommandRepository = commandRepository;
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
