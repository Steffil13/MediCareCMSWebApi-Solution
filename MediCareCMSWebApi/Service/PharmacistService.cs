using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModels;

namespace MediCareCMS.Service.Services
{
    public class PharmacistService : IPharmacistService
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistService(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        // ---------------- Medicines ----------------

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


        // ---------------- Prescriptions ----------------

        public async Task<IEnumerable<PrescriptionViewModel>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _pharmacistRepository.GetAllPrescriptionsAsync();

            return prescriptions.Select(p => new PrescriptionViewModel
            {
                PrescriptionId = p.PrescriptionId,
                PatientId = p.Appointment?.PatientId ?? 0,
                DoctorId = p.Appointment?.DoctorId ?? 0,
                DatePrescribed = p.CreatedDate ?? DateTime.MinValue,
                Medicines = p.PrescribedMedicines.Select(m => new PrescribedMedicineViewModel
                {
                    MedicineName = m.Medicine?.MedicineName ?? "N/A",
                    Dosage = m.Dosage ?? "N/A"
                }).ToList()
            }).ToList();
        }


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
                    Dosage = m.Dosage ?? "N/A"
                }).ToList()
            };
        }



        // ---------------- Patient History ----------------

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




        // ---------------- Pharmacy Bill ----------------

        public async Task GenerateBillAsync(PharmacyBillViewModel billModel)
        {
            var bill = new PharmacyBill
            {
                PharmacyBillId = $"BILL-{DateTime.Now:yyyyMMddHHmmss}",
                PrescriptionId = billModel.PrescriptionId,
                PmedicineId = billModel.PmedicineId,
                PharmacistId = billModel.PharmacistId,
                TotalAmount = billModel.TotalAmount,
                IssuedDate = DateTime.Now,
                IsIssued = true
            };

            await _pharmacistRepository.GeneratePharmacyBillAsync(bill);
        }

    }
}
