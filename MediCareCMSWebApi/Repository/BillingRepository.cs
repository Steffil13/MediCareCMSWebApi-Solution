using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class BillingRepository : IBillingRepository
    {
        private readonly MediCareDbContext _context;
        private readonly DbSet<BillingDto> _dbSet;
        public BillingRepository(MediCareDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BillingDto>();
        }

        public async Task<IList<BillingDto>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<BillingDto?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(BillingDto billing)
        {
            await _dbSet.AddAsync(billing);


        }

    }
}
