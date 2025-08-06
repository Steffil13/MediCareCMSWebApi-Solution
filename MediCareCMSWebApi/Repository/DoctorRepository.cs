using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MediCareDbContext _context;

        public DoctorRepository(MediCareDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentDto>> GetTodayAppointmentsAsync(int doctorId)
        {
            var today = DateTime.Today;

            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == today)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientId = a.PatientId,
                    AppointmentTime = a.AppointmentTime,
                    AppointmentDate = a.AppointmentDate
                    // Add more fields as needed
                })
                .ToListAsync();
        }

        public async Task<CreatePrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto dto)
        {
            var entity = new Prescription
            {
                AppointmentId = dto.AppointmentId,
                Diagnosis = dto.Diagnosis,
                Notes = dto.Notes,
                CreatedDate = DateTime.Now
            };

            _context.Prescriptions.Add(entity);
            await _context.SaveChangesAsync();

            dto.PrescriptionId = entity.PrescriptionId;
            return dto;
        }

        public async Task<bool> AddPrescribedMedicineAsync(PrescribedMedicineDto dto)
        {
            var entity = new PrescribedMedicine
            {
                PrescriptionId = dto.PrescriptionId,
                MedicineId = dto.MedicineId,
                Dosage = dto.Dosage,
                Duration = dto.Duration,
                IsIssued = false
            };

            _context.PrescribedMedicines.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddLabTestRequestAsync(PrescribedLabTestDto dto)
        {
            var entity = new PrescribedLabTest
            {
                PrescriptionId = dto.PrescriptionId,
                LabName = dto.LabName
            };

            _context.PrescribedLabTests.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PrescriptionDetailDto?> GetPrescriptionByAppointmentNumberAsync(string appointmentNumber)
        {
            var result = await (from p in _context.Prescriptions
                                join a in _context.Appointments on p.AppointmentId equals a.AppointmentId
                                join d in _context.Doctors on a.DoctorId equals d.DoctorId
                                join pat in _context.Patients on a.PatientId equals pat.PatientId
                                where a.AppointmentNumber == appointmentNumber
                                select new PrescriptionDetailDto
                                {
                                    PrescriptionId = p.PrescriptionId,
                                    AppointmentNumber = a.AppointmentNumber,
                                    DoctorName = d.FirstName,
                                    PatientName = pat.FirstName,
                                    Date = a.AppointmentDate,
                                    Diagnosis = p.Diagnosis
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<int?> GetDoctorIdByRoleIdAsync(int roleId)
        {
            var doctor = await _context.Doctors
                                       .FirstOrDefaultAsync(d => d.RoleId == roleId);

            return doctor?.DoctorId;
        }

    }

}
