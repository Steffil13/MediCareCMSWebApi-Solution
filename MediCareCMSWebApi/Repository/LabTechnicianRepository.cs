using MediCareCMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class LabTechnicianRepository : ILabTechnicianRepository
    {
        private readonly MediCareDbContext _context;

        public LabTechnicianRepository(MediCareDbContext context)
        {
            _context = context;
        }

        #region Add Lab Test Inventory
        public async Task AddLabTestAsync(LabInventory lab)
        {
            await _context.LabInventories.AddAsync(lab);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region View All Lab Tests
        public async Task<IEnumerable<LabInventory>> GetAllLabTestsAsync()
        {
            return await _context.LabInventories.ToListAsync();
        }
        #endregion

        #region Get Lab Test by ID
        public async Task<LabInventory?> GetLabTestByIdAsync(int id)
        {
            return await _context.LabInventories.FindAsync(id);
        }
        #endregion

        #region Assign Lab Test to Patient
        public async Task AssignLabTestToPatientAsync(PrescribedLabTest labTest)
        {
            await _context.PrescribedLabTests.AddAsync(labTest);
            await _context.SaveChangesAsync();
        }
        #endregion



        #region View Patient Lab History
        public async Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId)
        {
            return await _context.PatientHistories
                .Where(h => h.PatientId == patientId)
                .Include(h => h.PlabTest)
                    .ThenInclude(p => p.Lab)
                .Include(h => h.TestResult)
                .ToListAsync();
        }
        #endregion

        #region View All Prescribed Lab Tests
        public async Task<IEnumerable<PrescribedLabTest>> GetAllPrescribedLabTestsAsync()
        {
            return await _context.PrescribedLabTests
                .Include(p => p.Lab)
                .Include(p => p.Prescription)
                .ThenInclude(p => p.Appointment)
                .ToListAsync();
        }
        #endregion

        #region Generate Lab Bill
        public async Task GenerateLabBillAsync(LabBill labBill)
        {
            await _context.LabBills.AddAsync(labBill);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}

