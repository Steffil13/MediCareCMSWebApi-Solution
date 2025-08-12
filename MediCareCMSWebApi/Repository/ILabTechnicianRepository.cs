using MediCareCMSWebApi.Models;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Repository
{
    public interface ILabTechnicianRepository
    {
        #region Add Lab Test Inventory
        Task AddLabTestAsync(LabInventory lab);
        #endregion

        #region View All Lab Tests
        Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model);
        #endregion

        #region Get Lab Test by ID
        Task<PrescribedLabTest?> GetLabTestByIdAsync(int id);
        #endregion

        #region Assign Lab Test to Patient (Prescription-based)
        Task AssignLabTestToPatientAsync(PrescribedLabTest labTest);
        #endregion

        #region View Patient Lab History
        Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId);
        #endregion

        #region Generate Lab Bill
        Task<LabBill> GenerateLabBillAsync(LabBillViewModel billModel);
        #endregion

        #region View All Assigned Lab Tests (All Patients)
        // In ILabTechnicianRepository.cs
        Task<IEnumerable<AssignedLabTestViewModel>> GetAllPrescribedLabTestsAsync();
        #endregion

        Task<bool> UpdateTestResultAsync(int id, UpdateTestResultViewModel model);


        Task<List<TestResultHistoryDto>> GetTestResultHistoryAsync();



    }
}
