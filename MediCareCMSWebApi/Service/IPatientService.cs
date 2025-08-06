using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public interface IPatientService
    {
        Task<IList<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int patientId);
        Task<int> RegisterPatientAsync(PatientDto patient);
        Task<bool> UpdatePatientAsync(int patientId, PatientDto patient);
        Task DeletePatientAsync(int patientId);
    }

}
