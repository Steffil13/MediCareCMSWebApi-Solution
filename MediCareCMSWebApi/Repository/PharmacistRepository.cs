using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Repository
{
    public class PharmacistRepository : IPharmacistRepository
    {
        private readonly MediCareDbContext _context;

        public PharmacistRepository(MediCareDbContext context)
        {
            _context = context;
        }

        #region AddMedicine

        public async Task AddMedicineAsync(MedicineInventory medicine)
        {
            await _context.MedicineInventories.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region GetMedicineById

        public async Task<MedicineInventory?> GetMedicineByIdAsync(int id)
        {
            return await _context.MedicineInventories.FindAsync(id);
        }

        #endregion

        #region GetAllPrescriptions

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.LabBills)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
               //.Where(p => p.PrescribedMedicines.Any(pm => !pm.IsIssued)) // Exclude issued
                .ToListAsync();
        }


        #endregion

        #region GetPrescriptionById

        public async Task<Prescription?> GetPrescriptionByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.PrescribedMedicines)
                    .ThenInclude(pm => pm.Medicine)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        #endregion

        #region GetPatientHistory

        public async Task<IEnumerable<PatientHistory>> GetPatientHistoryAsync(int patientId)
        {
            return await _context.PatientHistories
                .Where(h => h.PatientId == patientId)
                .Include(h => h.Pmedicine)
                    .ThenInclude(pm => pm.Medicine)
                .Include(h => h.PlabTest)
                    .ThenInclude(pt => pt.Lab)
                .Include(h => h.TestResult)
                .ToListAsync();
        }

        #endregion

        #region GeneratePharmacyBill

        public async Task GeneratePharmacyBillAsync(PharmacyBill bill)
        {
            await _context.PharmacyBills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region IssueMedicine

        public async Task<PrescribedMedicine?> GetPrescribedMedicineByIdAsync(int id)
        {
            return await _context.PrescribedMedicines
                .Include(pm => pm.Medicine)
                .FirstOrDefaultAsync(pm => pm.PmedicineId == id);
        }

        public async Task UpdatePrescribedMedicineAsync(PrescribedMedicine prescribedMedicine)
        {
            _context.PrescribedMedicines.Update(prescribedMedicine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicineInventoryAsync(MedicineInventory medicine)
        {
            _context.MedicineInventories.Update(medicine);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region View All Lab Tests
        public async Task<IEnumerable<MedicineInventory>> GetAllMedicinesAsync()
        {
            return await _context.MedicineInventories.ToListAsync();
        }
        #endregion


        public async Task<IEnumerable<PatientHistory>> GetAllPatientHistoriesAsync()
        {
            return await _context.PatientHistories
                .Include(ph => ph.Pmedicine)
                    .ThenInclude(pm => pm.Medicine)
                .Include(ph => ph.PlabTest)
                    .ThenInclude(pl => pl.Lab)
                .Include(ph => ph.TestResult)
                .ToListAsync();
        }


        public async Task<PharmacyBillViewModel?> GetBillByPrescribedMedicineIdAsync(int pmId)
        {
            return await _context.PharmacyBills
                .Include(p => p.Prescription)
                    .ThenInclude(pt => pt.Appointment)
                        .ThenInclude(a => a.Doctor)
                .Include(p => p.Prescription)
                    .ThenInclude(ap => ap.Appointment)
                        .ThenInclude(a => a.Patient)
                .Include(p => p.Prescription)
                    .ThenInclude(pm => pm.PrescribedMedicines)
                        .ThenInclude(m => m.Medicine)
                .Where(b => b.PmedicineId == pmId)
                .Select(b => new PharmacyBillViewModel
                {
                    PharmacyBillId = b.PharmacyBillId,
                    DoctorName = b.Prescription.Appointment.Doctor.FirstName + " " +
                                 b.Prescription.Appointment.Doctor.LastName,
                    PatientName = b.Prescription.Appointment.Patient.FirstName + " " +
                                  b.Prescription.Appointment.Patient.LastName,
                    Medicines = b.Prescription.PrescribedMedicines
                        .Select(pm => pm.Medicine.MedicineName)
                        .ToList(),
                    TotalAmount = b.TotalAmount,
                    IssuedDate = b.IssuedDate
                })
                .FirstOrDefaultAsync();
        }


        public async Task AddPharmacyBillAsync(PharmacyBill bill)
        {
            await _context.PharmacyBills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PharmacyBillHistoryViewModel>> GetAllPharmacyBillsAsync()
        {
            var bills = await _context.PharmacyBills
                .Include(pb => pb.Prescription)
                    .ThenInclude(pr => pr.Appointment)
                        .ThenInclude(ap => ap.Doctor)
                .Include(pb => pb.Prescription)
                    .ThenInclude(pr => pr.Appointment)
                        .ThenInclude(ap => ap.Patient)
                .Include(pb => pb.Pharmacist)
                .Select(pb => new PharmacyBillHistoryViewModel
                {
                    PharmacyId = pb.PharmacyId,
                    PharmacyBillId = pb.PharmacyBillId,
                    PatientName = pb.Prescription.Appointment.Patient.FirstName + " " +
                                  pb.Prescription.Appointment.Patient.LastName,
                    DoctorName = pb.Prescription.Appointment.Doctor.FirstName + " " +
                                 pb.Prescription.Appointment.Doctor.LastName,
                    PharmacistName = pb.Pharmacist.FirstName + " " +
                                     pb.Pharmacist.LastName,
                    TotalAmount = pb.TotalAmount ?? 0,
                    IssuedDate = pb.IssuedDate,
                    IsIssued = pb.IsIssued ?? false
                })
                .ToListAsync();

            return bills;
        }

    }
}
