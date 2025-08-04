using MediCareCMSWebApi.ViewModels;

namespace MediCareCMSWebApi.Service
{
    public interface IPharmacistService
    {
        #region AddMedicine

        Task AddMedicineAsync(MedicineViewModel model);

        #endregion

        #region GetMedicineById

        Task<MedicineViewModel?> GetMedicineByIdAsync(int id);

        #endregion

        #region GetAllPrescriptions

        Task<IEnumerable<PrescriptionViewModel>> GetAllPrescriptionsAsync();

        #endregion

        #region GetPrescriptionById

        Task<PrescriptionViewModel?> GetPrescriptionByIdAsync(int id);

        #endregion

        #region GetPatientHistory

        Task<IEnumerable<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId);

        #endregion

        #region GenerateBill

        Task GenerateBillAsync(PharmacyBillViewModel billModel);

        #endregion
    }
}
