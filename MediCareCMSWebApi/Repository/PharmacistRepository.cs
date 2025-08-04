using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repositories
{
    public class PharmacistRepository : IPharmacistRepository
    {
        private readonly MediCareDbContext _context;

        public PharmacistRepository(MediCareDbContext context)
        {
            _context = context;
        }

        // ---------------- Medicines ----------------

        public async Task AddMedicineAsync(MedicineInventory medicine)
        {
            await _context.MedicineInventories.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task<MedicineInventory?> GetMedicineByIdAsync(int id)
        {
            return await _context.MedicineInventories.FindAsync(id);
        }

        // ---------------- Prescriptions ----------------

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .ToListAsync();
        }

        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        // ---------------- Patient History ----------------

        public async Task<IEnumerable<PatientHistory>> GetPatientHistoryAsync(int patientId)
        {
            return await _context.PatientHistories
                .Where(h => h.PatientId == patientId)
                .Include(h => h.Pmedicine)
                    .ThenInclude(pm => pm.Medicine)
                .Include(h => h.PlabTest)
                    .ThenInclude(pt => pt.Lab)
                .Include(h => h.TestResult)
                .ToListAsync();
        }

        // ---------------- Pharmacy Bill ----------------

        public async Task GeneratePharmacyBillAsync(PharmacyBill bill)
        {
            await _context.PharmacyBills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }
    }
}
