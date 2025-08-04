using MediCareCMSWebApi.Models;

namespace MediCareCMSWebApi.Repository
{
    public interface IPharmacistRepository
    {
        // ---------------- Medicines ----------------
        Task AddMedicineAsync(MedicineInventory medicine);
        Task<MedicineInventory?> GetMedicineByIdAsync(int id);


        // ---------------- Prescriptions ----------------
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription?> GetPrescriptionByIdAsync(int id);


        // ---------------- Patient History ----------------
        Task<IEnumerable<PatientHistory>> GetPatientHistoryAsync(int patientId);


        // ---------------- Pharmacy Bill ----------------
        Task GeneratePharmacyBillAsync(PharmacyBill bill);
    }
}
