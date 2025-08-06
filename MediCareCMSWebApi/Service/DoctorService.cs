using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<AppointmentDto>> GetTodayAppointmentsAsync(int doctorId)
        {
            return await _doctorRepository.GetTodayAppointmentsAsync(doctorId);
        }

        public async Task<CreatePrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto)
        {
            return await _doctorRepository.CreatePrescriptionAsync(prescriptionDto);
        }

        public async Task<bool> AddPrescribedMedicineAsync(PrescribedMedicineDto medicineDto)
        {
            return await _doctorRepository.AddPrescribedMedicineAsync(medicineDto);
        }

        public async Task<bool> AddLabTestRequestAsync(PrescribedLabTestDto labTestDto)
        {
            return await _doctorRepository.AddLabTestRequestAsync(labTestDto);
        }
        public async Task<PrescriptionDetailDto?> GetPrescriptionByAppointmentNumberAsync(string appointmentNumber)
        {
            return await _doctorRepository.GetPrescriptionByAppointmentNumberAsync(appointmentNumber);
        }
        public async Task<int?> GetDoctorIdByRoleIdAsync(int roleId)
        {
            return await _doctorRepository.GetDoctorIdByRoleIdAsync(roleId);
        }

    }

}
