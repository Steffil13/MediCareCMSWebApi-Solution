using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class BillingRepository : IBillingRepository
    {
        private readonly MediCareDbContext _context;

        public BillingRepository(MediCareDbContext context)
        {
            _context = context;
        }

        public async Task<IList<BillingDto>> GetAllAsync()
        {
            return await _context.ConsultationBills
                .Select(b => new BillingDto
                {
                    BillId = b.BillId,
                    BillNumber = b.BillNumber,
                    DateTime = b.DateTime,
                    CreatedDate = b.CreatedDate,
                    PatientId = b.PatientId,
                    DoctorId = b.DoctorId,
                    ReceptionistId = b.ReceptionistId,
                    AppointmentId = b.AppointmentId
                })
                .ToListAsync();
        }

        public async Task<BillingDto?> GetByIdAsync(int id)
        {
            return await _context.ConsultationBills
                .Where(b => b.BillId == id)
                .Select(b => new BillingDto
                {
                    BillId = b.BillId,
                    BillNumber = b.BillNumber,
                    DateTime = b.DateTime,
                    CreatedDate = b.CreatedDate,
                    PatientId = b.PatientId,
                    DoctorId = b.DoctorId,
                    ReceptionistId = b.ReceptionistId,
                    AppointmentId = b.AppointmentId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(BillingDto billingDto)
        {
            var bill = new ConsultationBill
            {
                BillNumber = billingDto.BillNumber,
                DateTime = billingDto.DateTime,
                CreatedDate = billingDto.CreatedDate ?? DateTime.Now,
                PatientId = billingDto.PatientId,
                DoctorId = billingDto.DoctorId,
                ReceptionistId = billingDto.ReceptionistId,
                AppointmentId = billingDto.AppointmentId
            };

            await _context.ConsultationBills.AddAsync(bill);
            await _context.SaveChangesAsync();
            return bill.BillId;

            // Optional: set generated ID back to DTO
            //billingDto.BillId = bill.BillId;
        }
    }
}
