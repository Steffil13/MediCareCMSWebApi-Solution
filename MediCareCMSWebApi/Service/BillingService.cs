using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;

        public BillingService(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public Task<IList<BillingDto>> GetAllBillingsAsync()
        {
            return _billingRepository.GetAllAsync();
        }

        public Task<BillingDto?> GetBillingByIdAsync(int id)
        {
            return _billingRepository.GetByIdAsync(id);
        }

        public Task<int>  AddBillingAsync(BillingDto billing)
        {
            return _billingRepository.AddAsync(billing);
        }
    }

}
