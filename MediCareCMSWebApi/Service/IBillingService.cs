using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public interface IBillingService
    {
        Task<IList<BillingDto>> GetAllBillingsAsync();
        Task<BillingDto?> GetBillingByIdAsync(int id);
        Task<int> AddBillingAsync(BillingDto billing);
    }
}
