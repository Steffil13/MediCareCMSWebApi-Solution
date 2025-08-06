using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Service
{
    public interface ILabTechnicianService
    {
        #region Lab Inventory

        Task<int> AddLabTestAsync(LabTechnicianViewModels.AddLabTestViewModel model);
        Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model);
        Task<LabInventory?> GetLabTestByIdAsync(int id);

        #endregion

        #region Prescribed Lab Tests

        Task AssignLabTestAsync(LabTechnicianViewModels.AssignLabTestViewModel model);

        #endregion

        #region Patient History

        Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId);

        #endregion

        Task<IEnumerable<AssignedLabTestViewModel>> GetAllAssignedLabTestsAsync();


        #region Lab Records

        Task<List<TestResultViewModel>> GetAllTestResultsAsync();

        #endregion

        #region lab bill

        Task GenerateLabBillAsync(LabBillViewModel billModel);

        #endregion

    }
}
