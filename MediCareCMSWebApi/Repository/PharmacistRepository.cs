using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class PharmacistRepository : IPharmacistRepository
    {
        private readonly MediCareDbContext _context;

        public PharmacistRepository(MediCareDbContext context)
        {
            _context = context;
        }

        #region AddMedicine

        public async Task AddMedicineAsync(MedicineInventory medicine)
        {
            await _context.MedicineInventories.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region GetMedicineById

        public async Task<MedicineInventory?> GetMedicineByIdAsync(int id)
        {
            return await _context.MedicineInventories.FindAsync(id);
        }

        #endregion

        #region GetAllPrescriptions

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .ToListAsync();
        }

        #endregion

        #region GetPrescriptionById

        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        #endregion

        #region GetPatientHistory

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

        #endregion

        #region GeneratePharmacyBill

        public async Task GeneratePharmacyBillAsync(PharmacyBill bill)
        {
            await _context.PharmacyBills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region IssueMedicine - Helpers

        public async Task<PrescribedMedicine?> GetPrescribedMedicineByIdAsync(int id)
        {
            return await _context.PrescribedMedicines
                .Include(pm => pm.Medicine)
                .FirstOrDefaultAsync(pm => pm.PmedicineId == id);
        }

        public async Task UpdatePrescribedMedicineAsync(PrescribedMedicine prescribedMedicine)
        {
            _context.PrescribedMedicines.Update(prescribedMedicine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicineInventoryAsync(MedicineInventory medicine)
        {
            _context.MedicineInventories.Update(medicine);
            await _context.SaveChangesAsync();
        }

        #endregion




    }
}
