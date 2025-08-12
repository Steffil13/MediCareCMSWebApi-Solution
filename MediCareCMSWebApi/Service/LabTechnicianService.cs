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
            var labInventory = new LabInventory
            {
                LabName = model.LabName,
                NormalRange = model.NormalRange,
                Price = model.Price,
                Availability = model.Availability
                // Map other properties as needed
            };

            await _labTechnicianRepository.AddLabTestAsync(labInventory);

            // If you want to return the newly created LabInventory ID, 
            // you should return it from repository method or fetch it after save.

            return labInventory.LabId; // Assuming LabId is generated on save
        }

        #endregion

        #region View All Lab Tests
        public Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model)
        {
            return _labTechnicianRepository.GetAllLabTestsAsync(model);
        }
        #endregion

        #region Get Lab Test By Id
        public Task<LabTestDetailsDto?> GetLabTestByIdAsync(int id)
        {
            return _labTechnicianRepository.GetLabTestByIdAsync(id);
        }

        #endregion
        #region Get Lab Bill By Id
        public Task<LabBill> GetBillByIdAsync(int id)
        {
            return _labTechnicianRepository.GetBillByIdAsync(id);
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
            return await _labTechnicianRepository.GenerateLabBillAsync(billModel);
        }

        #endregion

        public async Task<List<TestResultHistoryDto>> GetTestResultHistoryAsync()
        {
            return await _labTechnicianRepository.GetTestResultHistoryAsync();
        }


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
