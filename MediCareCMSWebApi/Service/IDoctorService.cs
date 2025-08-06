using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public interface IDoctorService
    {
        Task<List<AppointmentDto>> GetTodayAppointmentsAsync(int doctorId);
        Task<CreatePrescriptionDto> CreatePrescriptionAsync(CreatePrescriptionDto prescriptionDto);
        Task<bool> AddPrescribedMedicineAsync(PrescribedMedicineDto medicineDto);
        Task<bool> AddLabTestRequestAsync(PrescribedLabTestDto labTestDto);
        Task<PrescriptionDetailDto?> GetPrescriptionByAppointmentNumberAsync(string appointmentNumber);

        Task<int?> GetDoctorIdByRoleIdAsync(int roleId);

    }

}
