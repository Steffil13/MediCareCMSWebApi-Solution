using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId);
        Task<int> ScheduleAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(int AppointmentId, Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int appointmentId);
        Task<int> ScheduleAppointmentAsync(AppointmentViewModel dto);
    }
}
