using MediCareCMSWebApi.Models;

namespace MediCareCMSWebApi.Repository
{
    public interface ILabTechnicianRepository
    {
        #region Add Lab Test Inventory
        Task AddLabTestAsync(LabInventory lab);
        #endregion

        #region View All Lab Tests
        Task<IEnumerable<LabInventory>> GetAllLabTestsAsync();
        #endregion

        #region Get Lab Test by ID
        Task<LabInventory?> GetLabTestByIdAsync(int id);
        #endregion

        #region Assign Lab Test to Patient (Prescription-based)
        Task AssignLabTestToPatientAsync(PrescribedLabTest labTest);
        #endregion

        #region View Patient Lab History
        Task<IEnumerable<PatientHistory>> GetPatientLabHistoryAsync(int patientId);
        #endregion

        #region View All Prescribed Lab Tests
        Task<IEnumerable<PrescribedLabTest>> GetAllPrescribedLabTestsAsync();
        #endregion

        #region Generate Lab Bill
        Task GenerateLabBillAsync(LabBill labBill);
        #endregion
    }
}
