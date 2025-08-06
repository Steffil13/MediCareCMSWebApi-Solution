using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Service
{
    public class LabTechnicianService : ILabTechnicianService
    {
        private readonly MediCareDbContext _context;

        public LabTechnicianService(MediCareDbContext context)
        {
            _context = context;
        }

        #region Add Lab Test Inventory
        public async Task<int> AddLabTestAsync(LabTechnicianViewModels.AddLabTestViewModel model)
        {
            var entity = new LabInventory
            {
                LabName = model.LabName,
                NormalRange = model.NormalRange,
                Price = model.Price ?? 0,
                Availability = model.Availability ?? true
            };

            _context.LabInventories.Add(entity);
            await _context.SaveChangesAsync();

            return entity.LabId; // returning just the ID
        }
        #endregion

        #region View All Lab Tests
        public async Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model)
        {
            return await _context.LabInventories.ToListAsync();
        }
        #endregion

        #region Get Lab Test By Id
        public async Task<LabInventory?> GetLabTestByIdAsync(int id)
        {
            return await _context.LabInventories.FindAsync(id);
        }
        #endregion

        #region Assign Lab Test to a Patient
        public async Task AssignLabTestAsync(AssignLabTestViewModel model)
        {
            // Step 1: Add prescribed lab test
            var prescribedTest = new PrescribedLabTest
            {
                PrescriptionId = model.PrescriptionId,
                LabId = model.LabId,
                IsCompleted = false
            };

            _context.PrescribedLabTests.Add(prescribedTest);
            await _context.SaveChangesAsync(); // Save to get PlabTestId

            // Step 2: Add test result
            var testResult = new TestResult
            {
                PlabTestId = prescribedTest.PlabTestId,  // Use generated ID
                ResultValue = model.ResultValue,
                ResultStatus = model.ResultStatus,
                Remarks = model.Remarks,
                RecordDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();
        }



        #endregion

        #region View Patient Lab History
        public async Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId)
        {
            return await _context.PatientHistories
                .Where(ph => ph.PatientId == patientId)
                .Include(ph => ph.PlabTest)
                    .ThenInclude(pl => pl.Lab)
                .Include(ph => ph.TestResult)
                .ToListAsync();
        }
        #endregion

        #region View All Lab Records
        public async Task<List<TestResultViewModel>> GetAllTestResultsAsync()
        {
            return await _context.TestResults
                .Include(tr => tr.PlabTest)
                .ThenInclude(pt => pt.Lab)
                .Select(tr => new TestResultViewModel
                {
                    TestResultId = tr.TestResultId,
                    ResultValue = tr.ResultValue,
                    ResultStatus = tr.ResultStatus,
                    Remarks = tr.Remarks,
                    RecordDate = tr.RecordDate,
                    CreatedDate = tr.CreatedDate,
                    PlabTestId = tr.PlabTestId,
                    LabName = tr.PlabTest.Lab.LabName
                })
                .ToListAsync();
        }
        #endregion

        #region bill

        public async Task GenerateLabBillAsync(LabBillViewModel billModel)
        {
            var bill = new LabBill
            {
                LabBillNumber = $"LABBILL-{DateTime.Now:yyyyMMddHHmmss}",
                PrescriptionId = billModel.PrescriptionId,
                LabTechnicianId = billModel.LabTechnicianId,
                PatientId = billModel.PatientId,
                DoctorId = billModel.DoctorId,
                TotalAmount = billModel.TotalAmount,
                IssuedDate = DateTime.Now,
                IsPaid = true
            };

            await _context.LabBills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }
        #endregion
        public async Task<IEnumerable<AssignedLabTestViewModel>> GetAllAssignedLabTestsAsync()
        {
            return await _context.PrescribedLabTests
                .Include(p => p.Lab)
                .Include(p => p.Prescription)
                    .ThenInclude(pr => pr.Appointment)
                        .ThenInclude(ap => ap.Doctor)
                .Include(p => p.Prescription)
                    .ThenInclude(pr => pr.Appointment)
                        .ThenInclude(ap => ap.Patient)
                .Select(p => new AssignedLabTestViewModel
                {
                    PlabTestId = p.PlabTestId,
                    LabId = p.LabId,
                    LabName = p.Lab.LabName,
                    Price = p.Lab.Price ?? 0,
                    NormalRange = p.Lab.NormalRange ?? string.Empty,
                    PrescriptionId = p.PrescriptionId,
                    DoctorId = p.Prescription.Appointment.DoctorId,
                    PatientId = p.Prescription.Appointment.PatientId,
                    DoctorName = p.Prescription.Appointment.Doctor.FirstName + " " + p.Prescription.Appointment.Doctor.LastName,
                    PatientName = p.Prescription.Appointment.Patient.FirstName + " " + p.Prescription.Appointment.Patient.LastName,
                    Date = p.Prescription.CreatedDate ?? DateTime.MinValue,
                    IsCompleted = p.IsCompleted ?? false
                })
                .ToListAsync();
        }






    }
}
