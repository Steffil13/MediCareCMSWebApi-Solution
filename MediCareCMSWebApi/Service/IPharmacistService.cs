using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModels;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

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

        Task<PharmacyBill> GenerateBillAsync(PharmacyBillViewModel model);

        #endregion

        Task<bool> IssueMedicineAsync(int prescribedMedicineId);

        Task<List<BillHistory>> GetBillHistoryAsync();

        Task<IEnumerable<MedicineInventory>> GetAllMedicinesAsync();

        Task<PharmacyBillViewModel?> GetBillByPrescribedMedicineIdAsync(int pmId);


        Task<IEnumerable<PatientHistoryViewModel>> GetAllPatientHistoriesAsync();


    }
}
