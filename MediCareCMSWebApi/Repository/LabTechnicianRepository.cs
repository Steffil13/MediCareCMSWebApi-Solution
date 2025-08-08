using MediCareCMSWebApi.Models;
using Microsoft.EntityFrameworkCore;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

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
        public async Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model)
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

 
        #region Generate Lab Bill
        public async Task GenerateLabBillAsync(LabBill labBill)
        {
            await _context.LabBills.AddAsync(labBill);
            await _context.SaveChangesAsync();
        }
        #endregion

        // In LabTechnicianRepository.cs
        public async Task<IEnumerable<AssignedLabTestViewModel>> GetAllPrescribedLabTestsAsync()
        {
            return await _context.PrescribedLabTests
                .Where(pl => pl.IsCompleted == false || pl.IsCompleted == null)
                .Select(pl => new AssignedLabTestViewModel
                {
                    PlabTestId = pl.PlabTestId,
                    Remarks = pl.TestResults.FirstOrDefault().Remarks,
                    PrescriptionId = pl.PrescriptionId,
                    LabId = pl.LabId,
                    LabName = pl.Lab.LabName,
                    Price = (decimal)pl.Lab.Price,
                    NormalRange = pl.Lab.NormalRange,
                    DoctorId = pl.Prescription.Appointment.DoctorId,
                    PatientId = pl.Prescription.Appointment.PatientId,
                    RegisterNumber = pl.Prescription.Appointment.Patient.RegisterNumber,
                    DoctorName = pl.Prescription.Appointment.Doctor.FirstName + " " + pl.Prescription.Appointment.Doctor.LastName,
                    PatientName = pl.Prescription.Appointment.Patient.FirstName + " " + pl.Prescription.Appointment.Patient.LastName,
                    Date = pl.Prescription.Appointment.AppointmentDate,
                    // Pull Remarks directly from TestResults
                    //Remarks = _context.TestResults
                    //    .Where(tr => tr.PlabTestId == pl.PlabTestId)
                    //    .OrderByDescending(tr => tr.RecordDate)
                    //    .Select(tr => tr.Remarks)
                    //    .FirstOrDefault(),

                    // New: Pull the actual ResultValue
                    ResultValue = _context.TestResults
                        .Where(tr => tr.PlabTestId == pl.PlabTestId)
                        .OrderByDescending(tr => tr.RecordDate)
                        .Select(tr => tr.ResultValue)
                        .FirstOrDefault(),

                    IsCompleted = pl.IsCompleted ?? false
                })
                .ToListAsync();
        }


        public async Task<bool> UpdateTestResultAsync(int id, UpdateTestResultViewModel model)
        {
            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null)
                return false;

            testResult.ResultValue = model.ResultValue;
            testResult.ResultStatus = model.ResultStatus;
            testResult.Remarks = model.Remarks;
            testResult.RecordDate = DateTime.Now;

            _context.TestResults.Update(testResult);

            var prescribedLabTest = await _context.PrescribedLabTests
                .FirstOrDefaultAsync(p => p.PlabTestId == testResult.PlabTestId);

            if (prescribedLabTest != null)
            {
                prescribedLabTest.IsCompleted = true;
                _context.PrescribedLabTests.Update(prescribedLabTest);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

