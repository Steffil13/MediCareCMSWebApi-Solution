using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return _repository.GetAppointmentsByPatientIdAsync(patientId);
        }

        public Task<Appointment> GetAppointmentByIdAsync(string appointmentId)
        {
            return _repository.GetAppointmentByIdAsync(appointmentId);
        }

        public Task<bool> ScheduleAppointmentAsync(Appointment appointment)
        {
            return _repository.ScheduleAppointmentAsync(appointment);
        }

        public Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            return _repository.UpdateAppointmentAsync(appointment);
        }

        public Task<bool> DeleteAppointmentAsync(string appointmentId)
        {
            return _repository.DeleteAppointmentAsync(appointmentId);
        }

        public async Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId)
        {
            return await _repository.GetAppointmentsForDoctorAsync(doctorId);
        }
    }
}
