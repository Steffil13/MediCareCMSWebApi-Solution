using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Service
{
    public interface ILabTechnicianService
    {
        Task<int> AddLabTestAsync(AddLabTestViewModel model);

        // View All Lab Tests
        Task<IEnumerable<LabInventory>> GetAllLabTestsAsync(ViewAllLabTestsViewModel model);

        // Get Lab Test By ID
        Task<LabInventory?> GetLabTestByIdAsync(int id);

        // Assign Lab Test to Patient
        Task AssignLabTestAsync(AssignLabTestViewModel model);

        // View Patient Lab History
        Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId);

        // Generate Lab Bill
        Task GenerateLabBillAsync(LabBillViewModel billModel);

        // View All Prescribed Lab Tests
        Task<IEnumerable<AssignedLabTestViewModel>> GetAllPrescribedLabTestsAsync();

        // Update Test Result
        Task<bool> UpdateTestResultAsync(int id, UpdateTestResultViewModel model);
    }


}

