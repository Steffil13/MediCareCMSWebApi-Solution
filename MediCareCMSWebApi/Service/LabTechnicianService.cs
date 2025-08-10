using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Service
{
    public class LabTechnicianService : ILabTechnicianService
    {
        private readonly ILabTechnicianRepository _labTechnicianRepository;

        public LabTechnicianService(ILabTechnicianRepository labTechnicianRepository)
        {
            _labTechnicianRepository = labTechnicianRepository;
        }

        #region Add Lab Test Inventory
        public async Task<int> AddLabTestAsync(AddLabTestViewModel model)
        {
            var entity = new LabInventory
            {
                LabName = model.LabName,
                NormalRange = model.NormalRange,
                Price = model.Price ?? 0,
                Availability = model.Availability ?? true
            };

            await _labTechnicianRepository.AddLabTestAsync(entity);
            return entity.LabId;
        }
        #endregion

        #region View All Lab Tests
        public Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model)
        {
            return _labTechnicianRepository.GetAllLabTestsAsync(model);
        }
        #endregion

        #region Get Lab Test By Id
        public Task<LabInventory?> GetLabTestByIdAsync(int id)
        {
            return _labTechnicianRepository.GetLabTestByIdAsync(id);
        }
        #endregion

        #region Assign Lab Test to a Patient
        public async Task AssignLabTestAsync(AssignLabTestViewModel model)
        {
            var prescribedTest = new PrescribedLabTest
            {
                PrescriptionId = model.PrescriptionId,
                LabId = model.LabId,
                IsCompleted = false
            };

            await _labTechnicianRepository.AssignLabTestToPatientAsync(prescribedTest);
        }
        #endregion

        #region View Patient Lab History
        public Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId)
        {
            return _labTechnicianRepository.GetPatientLabHistoryAsync(patientId);
        }
        #endregion

        #region Generate Lab Bill
        public async Task<LabBill> GenerateLabBillAsync(LabBillViewModel billModel)
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

            await _labTechnicianRepository.GenerateLabBillAsync(bill);

            return bill;  // Return the created bill object
        }

        #endregion

        #region View All Prescribed Lab Tests
        public Task<IEnumerable<AssignedLabTestViewModel>> GetAllPrescribedLabTestsAsync()
        {
            return _labTechnicianRepository.GetAllPrescribedLabTestsAsync();
        }
        #endregion

        #region Update Test Result
        public Task<bool> UpdateTestResultAsync(int id, UpdateTestResultViewModel model)
        {
            return _labTechnicianRepository.UpdateTestResultAsync(id, model);
        }
        #endregion
    }
}
