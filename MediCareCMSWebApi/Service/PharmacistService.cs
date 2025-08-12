using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModels;
using NuGet.Protocol.Core.Types;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Service
{
    public class PharmacistService : IPharmacistService
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistService(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        #region AddMedicine

        public async Task AddMedicineAsync(MedicineViewModel model)
        {
            var medicine = new MedicineInventory
            {
                MedicineName = model.MedicineName,
                Quantity = model.Quantity,
                Price = model.Price,
                ManufactureDate = model.ManufactureDate,
                ExpiryDate = model.ExpiryDate,
                Availability = model.Availability
            };

            await _pharmacistRepository.AddMedicineAsync(medicine);
        }

        #endregion

        #region GetMedicineById

        public async Task<MedicineViewModel?> GetMedicineByIdAsync(int id)
        {
            var medicine = await _pharmacistRepository.GetMedicineByIdAsync(id);
            if (medicine == null) return null;

            return new MedicineViewModel
            {
                MedicineId = medicine.MedicineId,
                MedicineName = medicine.MedicineName ?? "N/A",
                Quantity = medicine.Quantity ?? 0,
                Price = medicine.Price ?? 0,
                ManufactureDate = medicine.ManufactureDate ?? DateTime.MinValue,
                ExpiryDate = medicine.ExpiryDate ?? DateTime.MinValue,
                Availability = medicine.Availability ?? false
            };
        }

        #endregion

        #region GetAllPrescriptions

        public async Task<IEnumerable<PrescriptionViewModel>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _pharmacistRepository.GetAllPrescriptionsAsync();

            return prescriptions.Select(p => new PrescriptionViewModel
            {
                PrescriptionId = p.PrescriptionId,
                PatientId = p.Appointment?.PatientId ?? 0,
                DoctorId = p.Appointment?.DoctorId ?? 0,

                DatePrescribed = p.CreatedDate ?? DateTime.MinValue,
                Medicines = p.PrescribedMedicines
                .Where(m => m.IsIssued != true)  // Only medicines NOT issued yet (false or null)
                .Select(m => new PrescribedMedicineViewModel
                    {
                        MedicineName = m.Medicine?.MedicineName ?? "N/A",
                        Dosage = m.Dosage ?? "N/A",
                        Duration = m.Duration ?? "N/A",
                        PMedicineId = m.PmedicineId,
                        IsIssued = m.IsIssued

                    }).ToList()
            }).ToList();
        }

        #endregion

        #region GetPrescriptionById

        public async Task<PrescriptionViewModel?> GetPrescriptionByIdAsync(int id)
        {
            var prescription = await _pharmacistRepository.GetPrescriptionByIdAsync(id);
            if (prescription == null) return null;

            return new PrescriptionViewModel
            {
                PrescriptionId = prescription.PrescriptionId,
                PatientId = prescription.Appointment?.PatientId ?? 0,
                DoctorId = prescription.Appointment?.DoctorId ?? 0,
                DatePrescribed = prescription.CreatedDate ?? DateTime.MinValue,
                Medicines = prescription.PrescribedMedicines.Select(m => new PrescribedMedicineViewModel
                {
                    MedicineName = m.Medicine?.MedicineName ?? "N/A",
                    Price = m.Medicine?.Price ?? 0,
                    Dosage = m.Dosage ?? "N/A",
                    Duration = m.Duration ?? "N/A",
                    PMedicineId = m.PmedicineId,
                    IsIssued = m.IsIssued
                    

                }).ToList()
            };
        }

        #endregion

        #region GetPatientHistory

        public async Task<IEnumerable<PatientHistoryViewModel>> GetPatientHistoryAsync(int patientId)
        {
            var historyList = await _pharmacistRepository.GetPatientHistoryAsync(patientId);

            return historyList.Select(h => new PatientHistoryViewModel
            {
                HistoryId = h.HistoryId,
                PatientId = h.PatientId ?? 0,
                AppointmentId = h.AppointmentId ?? 0,
                PrescriptionId = h.PrescriptionId ?? 0,
                MedicineName = h.Pmedicine?.Medicine?.MedicineName ?? "N/A",
                LabTestName = h.PlabTest?.Lab?.LabName ?? "N/A",
                TestResult = h.TestResult?.ResultValue ?? "Pending"
            }).ToList();
        }

        #endregion

        #region GenerateBill

        public async Task<PharmacyBill> GenerateBillAsync(PharmacyBillViewModel model)
        {
            var bill = new PharmacyBill
            {
                PharmacyBillId = model.PharmacyBillId,
                PmedicineId = model.PmedicineId,
                PrescriptionId = model.PrescriptionId,
                PharmacistId = model.PharmacistId,
                TotalAmount = model.TotalAmount,
                IssuedDate = DateTime.UtcNow,
                IsIssued = true
            };

            await _pharmacistRepository.AddPharmacyBillAsync(bill);
            return bill;
        }

        #endregion

        #region IssueMedicine

        public async Task<bool> IssueMedicineAsync(int prescribedMedicineId)
        {
            var prescribedMedicine = await _pharmacistRepository.GetPrescribedMedicineByIdAsync(prescribedMedicineId);

            if (prescribedMedicine == null || prescribedMedicine.IsIssued == true)
                return false;

            var medicine = prescribedMedicine.Medicine;
            if (medicine == null || medicine.Quantity == null || medicine.Quantity <= 0)
                return false;

            // Deduct quantity
            medicine.Quantity -= 1;

            // Update availability
            medicine.Availability = medicine.Quantity > 0;

            // Mark as issued
            prescribedMedicine.IsIssued = true;


            // Update both
            await _pharmacistRepository.UpdateMedicineInventoryAsync(medicine);
            await _pharmacistRepository.UpdatePrescribedMedicineAsync(prescribedMedicine);

            return true;
        }

        #endregion

        public async Task<IEnumerable<PharmacyBillHistoryViewModel>> GetAllPharmacyBillsAsync()
        {
            return await _pharmacistRepository.GetAllPharmacyBillsAsync();
        }


        public Task<IEnumerable<MedicineInventory>> GetAllMedicinesAsync()
        {
            return _pharmacistRepository.GetAllMedicinesAsync();
        }



        public async Task<PharmacyBillViewModel?> GetBillByPrescribedMedicineIdAsync(int pmId)
        {
            var bill = await _pharmacistRepository.GetBillByPrescribedMedicineIdAsync(pmId);

            if (bill == null) return null;

            return new PharmacyBillViewModel
            {
                PharmacyBillId = bill.PharmacyBillId ?? string.Empty,
                PmedicineId = bill.PmedicineId ?? 0,
                PrescriptionId = bill.PrescriptionId ?? 0,
                PharmacistId = bill.PharmacistId ?? 0,
                TotalAmount = bill.TotalAmount ?? 0m
            };
        }
        public async Task<IEnumerable<PatientHistoryViewModel>> GetAllPatientHistoriesAsync()
        {
            var histories = await _pharmacistRepository.GetAllPatientHistoriesAsync();

            return histories.Select(h => new PatientHistoryViewModel
            {
                HistoryId = h.HistoryId,
                PatientId = h.PatientId ?? 0,
                AppointmentId = h.AppointmentId ?? 0,
                PrescriptionId = h.PrescriptionId ?? 0,
                MedicineName = h.Pmedicine?.Medicine?.MedicineName ?? "N/A",
                LabTestName = h.PlabTest?.Lab?.LabName ?? "N/A",
                TestResult = h.TestResult?.ResultValue ?? "Pending"
            }).ToList();
        }

    }
}
