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

        public Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return _repository.GetAppointmentsByPatientIdAsync(patientId);
        }

        public Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return _repository.GetAppointmentByIdAsync(appointmentId);
        }

        public Task<int> ScheduleAppointmentAsync(Appointment appointment)
        {
            return _repository.ScheduleAppointmentAsync(appointment);
        }

        public Task<bool> UpdateAppointmentAsync(int AppointmentId,Appointment appointment)
        {
            return _repository.UpdateAppointmentAsync(AppointmentId, appointment);
        }

        public Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            return _repository.DeleteAppointmentAsync(appointmentId);
        }

        public async Task<List<AppointmentDto>> GetAppointmentsForDoctorAsync(int doctorId)
        {
            return await _repository.GetAppointmentsForDoctorAsync(doctorId);
        }
        public async Task<int> ScheduleAppointmentAsync(AppointmentViewModel dto)
        {
            return await _repository.AddAsync(dto);
        }
    }
}
