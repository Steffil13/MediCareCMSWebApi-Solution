using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly MediCareDbContext _context;

        public AppointmentRepository(MediCareDbContext context)
        {
            _context = context;
        }

        // Fetch all appointments by a given patient ID
        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .Include(a => a.Patient)
                                 .Select(a => new AppointmentDto
                                 {
                                     AppointmentId = a.AppointmentId,
                                     AppointmentDate = a.AppointmentDate,
                                     PatientName = a.Patient != null
                                         ? a.Patient.FirstName + " " + a.Patient.LastName
                                         : "Unknown",
                                     TokenNumber = a.TokenNumber,
                                     AppointmentTime = a.AppointmentTime
                                     // Add other fields if needed
                                 })
                                 .ToListAsync();
        }


        // Find appointment by primary key (assuming int, change to string if AppointmentId is string)
        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        // Schedules a new appointment (using Appointment entity)
        public async Task<bool> ScheduleAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Update existing appointment
        public async Task<bool> UpdateAppointmentAsync(int AppointmentId, Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Delete appointment
        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        // Get appointment summary for a doctor
        public async Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Patient) // Ensure navigation property exists.
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    AppointmentNumber = a.AppointmentNumber,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentTime = a.AppointmentTime,
                    TokenNumber = a.TokenNumber,
                    CreatedDate = a.CreatedDate,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                    ReceptionistId = a.ReceptionistId,
                    IsConsulted = a.IsConsulted
                })
                .ToListAsync();
        }

        //vineesha
        // Add appointment with token generation logic (from DTO)
        public async Task<int> AddAsync(AppointmentViewModel dto)
        {
            var today = DateTime.Now.Date; // Or DateTime.UtcNow.Date for UTC

            // Get the maximum token for doctor on today's date
            var maxToken = await _context.Appointments
                .Where(a => a.DoctorId == dto.DoctorId && a.AppointmentDate.Date == today)
                .MaxAsync(a => (int?)a.TokenNumber) ?? 0;

            if (maxToken >= 30)
                throw new InvalidOperationException("All tokens for the doctor are booked for today.");

            var appointment = new Appointment
            {
                AppointmentDate = DateTime.Now, // Use system time
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                ReceptionistId = dto.ReceptionistId,
                AppointmentNumber = dto.AppointmentNumber ?? Guid.NewGuid().ToString(), // Generate a unique appointment number if not provided
                //Notes = dto.Notes,
                TokenNumber = maxToken + 1,
                AppointmentTime = DateTime.Now.ToString("HH:mm:ss") // optionally store time if this property exists and is string
            };

            Console.WriteLine($"AppointmentId BEFORE insert: {appointment.AppointmentId}");

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment.AppointmentId;
        }

       
    }
}
