using System.Threading.Tasks;
using PlatformService.Interfaces;

namespace PlatformService.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPlatformRepository PlatformRepository { get; }

        public UnitOfWork(IPlatformRepository platformRepository, AppDbContext context)
        {
            PlatformRepository = platformRepository;
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
