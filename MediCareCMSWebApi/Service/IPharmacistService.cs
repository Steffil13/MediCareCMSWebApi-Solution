using MediCareCMSWebApi.ViewModels;

namespace MediCareCMSWebApi.Service
{
    public interface IPharmacistService
    {
        // ---------------- Medicines ----------------

        Task AddMedicineAsync(MedicineViewModel model);
        Task<MedicineViewModel?> GetMedicineByIdAsync(int id);


        // ---------------- Prescriptions ----------------

        Task<IEnumerable<PrescriptionViewModel>> GetAllPrescriptionsAsync();
        Task<PrescriptionViewModel?> GetPrescriptionByIdAsync(int id);


        // ---------------- Patient History ----------------

        Task<IEnumerable<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId);


        // ---------------- Pharmacy Bill ----------------

        Task GenerateBillAsync(PharmacyBillViewModel billModel);
    }
}
