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

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        public async Task<bool> ScheduleAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(string appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient) // assuming navigation property
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                    AppointmentDate = (DateTime)a.CreatedDate,
                    AppointmentTime = a.AppointmentTime
                })
                .ToListAsync();
        }
    }
}
