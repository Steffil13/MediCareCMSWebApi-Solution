using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Repository
{
    public interface IBillingRepository
    {
        Task<IList<BillingDto>> GetAllAsync();
        Task<BillingDto?> GetByIdAsync(int id);
        Task<int> AddAsync(BillingDto billing);
    }
}
