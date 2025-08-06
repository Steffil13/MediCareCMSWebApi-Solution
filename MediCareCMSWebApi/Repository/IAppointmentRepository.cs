using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Repository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<bool> ScheduleAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(int AppointmentId, Appointment appointmentt);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
        // for add appointment
        Task<int> AddAsync(AppointmentViewModel dto);
        
    }
}
