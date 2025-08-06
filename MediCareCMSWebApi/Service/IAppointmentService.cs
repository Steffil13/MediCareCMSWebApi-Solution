using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<Appointment> GetAppointmentByIdAsync(string appointmentId);
        Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId);
        Task<bool> ScheduleAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(string appointmentId);
    }
}
