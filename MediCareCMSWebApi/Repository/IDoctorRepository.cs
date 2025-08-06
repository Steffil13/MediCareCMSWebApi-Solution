using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Repository
{
    public interface IDoctorRepository
    {
        Task<List<AppointmentDto>> GetTodayAppointmentsAsync(int doctorId);
        Task<CreatePrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto);
        Task<bool> AddPrescribedMedicineAsync(PrescribedMedicineDto medicineDto);
        Task<bool> AddLabTestRequestAsync(PrescribedLabTestDto labTestDto);
        Task<PrescriptionDetailDto?> GetPrescriptionByAppointmentNumberAsync(string appointmentNumber);
        Task<int?> GetDoctorIdByRoleIdAsync(int roleId);

    }

}
