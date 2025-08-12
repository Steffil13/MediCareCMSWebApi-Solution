using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModels;
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
        public async Task<LabTestDetailsDto?> GetLabTestByIdAsync(int id)
        {
            return await _context.PrescribedLabTests
                .Where(pl => pl.PlabTestId == id)
                .Select(pl => new LabTestDetailsDto
                {
                    PlabTestId = pl.PlabTestId,
                    PrescriptionId = pl.PrescriptionId,
                    IsCompleted = pl.IsCompleted,
                    LabId = pl.LabId,
                    LabName = pl.Lab.LabName,               // Assuming Lab navigation is configured
                    LabTechnicianId = pl.LabTechnicianId,
                    DoctorId = pl.Prescription.Appointment.DoctorId,
                    PatientId = pl.Prescription.Appointment.PatientId
                })
                .FirstOrDefaultAsync();
        }

        #endregion

        #region get labbill by id
        public async Task<LabBill> GetBillByIdAsync(int id)
        {
            var values = await _context.LabBills
                .Include(a=> a.Doctor)
                .Include(a=> a.Patient)
                .FirstOrDefaultAsync(d => d.LabBillId == id);
            return values;
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
        public async Task<LabBill> GenerateLabBillAsync(LabBillViewModel billModel)
        {
            var labBill = new LabBill
            {
                LabBillNumber = $"LABBILL-{DateTime.UtcNow:yyyyMMddHHmmss}",
                PatientId = billModel.PatientId,
                
                PrescriptionId = billModel.PrescriptionId,
                DoctorId = billModel.DoctorId,
                LabTechnicianId = 1,
                TotalAmount = 300,
                IssuedDate = DateTime.UtcNow,
                IsPaid = true
            };

            _context.LabBills.Add(labBill);
            await _context.SaveChangesAsync();

            return labBill;
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
                    ResultValue = pl.TestResults.FirstOrDefault().ResultValue,
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
                    IsCompleted = pl.IsCompleted ?? false
                })
                .ToListAsync();
        }


        public async Task<bool> UpdateTestResultAsync(int plabTestId, UpdateTestResultViewModel model)
        {
            // Try to find existing TestResult
            var testResult = await _context.TestResults
                .FirstOrDefaultAsync(t => t.PlabTestId == plabTestId);

            if (testResult == null)
            {
                // Create new TestResult if none exists
                testResult = new TestResult
                {
                    PlabTestId = plabTestId,
                    ResultValue = model.ResultValue,
                    ResultStatus = model.ResultStatus,
                    Remarks = model.Remarks,
                    RecordDate = DateTime.Now
                };

                _context.TestResults.Add(testResult);
            }
            else
            {
                // Update existing TestResult
                testResult.ResultValue = model.ResultValue;
                testResult.ResultStatus = model.ResultStatus;
                testResult.Remarks = model.Remarks;
                testResult.RecordDate = DateTime.Now;

                _context.TestResults.Update(testResult);
            }

            var prescribedLabTest = await _context.PrescribedLabTests
                .FirstOrDefaultAsync(p => p.PlabTestId == plabTestId);

            if (prescribedLabTest != null)
            {
                prescribedLabTest.IsCompleted = true;
                _context.PrescribedLabTests.Update(prescribedLabTest);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<LabBill>> GetBillHistoryAsync()
        {
            return await _context.LabBills
                .Include(b => b.Patient)
                .Include(b => b.Doctor)
                .Include(b => b.Prescription)
                .OrderByDescending(b => b.IssuedDate)
                .ToListAsync();
        }

        public async Task<List<TestResultHistoryDto>> GetTestResultHistoryAsync()
        {
            return await _context.TestResults
                .Select(tr => new TestResultHistoryDto
                {
                       RegisterNumber = tr.PlabTest.Prescription.Appointment.Patient.RegisterNumber,

                    TestResultId = tr.TestResultId,
                    PatientName = tr.PlabTest.Prescription.Appointment.Patient.FirstName + " " +
                                  tr.PlabTest.Prescription.Appointment.Patient.LastName,
                    TestName = tr.PlabTest.Lab.LabName, // from LabInventory
                    ResultValue = tr.ResultValue,
                    Remarks = tr.Remarks,
                    RecordDate = tr.RecordDate,
                    ResultStatus = tr.ResultStatus
                })
                .ToListAsync();
        }




    }
}

