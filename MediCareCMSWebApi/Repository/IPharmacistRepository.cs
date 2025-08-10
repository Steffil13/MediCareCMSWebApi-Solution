using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModels;

namespace MediCareCMSWebApi.Repository
{
    public interface IPharmacistRepository
    {
        #region AddMedicine

        Task AddMedicineAsync(MedicineInventory medicine);

        #endregion

        #region GetMedicineById

        Task<MedicineInventory?> GetMedicineByIdAsync(int id);

        #endregion

        #region GetAllPrescriptions

        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();

        #endregion

        #region GetPrescriptionById

        Task<Prescription?> GetPrescriptionByIdAsync(int id);

        #endregion

        #region GetPatientHistory

        Task<IEnumerable<PatientHistory>> GetPatientHistoryAsync(int patientId);

        #endregion

        #region GeneratePharmacyBill

        Task GeneratePharmacyBillAsync(PharmacyBill bill);

        #endregion

        Task<PrescribedMedicine?> GetPrescribedMedicineByIdAsync(int id);
        Task UpdatePrescribedMedicineAsync(PrescribedMedicine prescribedMedicine);
        Task UpdateMedicineInventoryAsync(MedicineInventory medicine);
        Task<IEnumerable<MedicineInventory>> GetAllMedicinesAsync();


    }
}
