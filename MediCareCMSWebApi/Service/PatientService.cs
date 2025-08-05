using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<IList<PatientDto>> GetAllPatientsAsync()
        {
            return _patientRepository.GetAllAsync();
        }

        public Task<PatientDto?> GetPatientByIdAsync(int patientId)
        {
            return _patientRepository.GetByIdAsync(patientId);
        }

        public Task<int> RegisterPatientAsync(PatientDto patient)
        {
            return _patientRepository.AddAsync(patient);
        }

        public Task<bool> UpdatePatientAsync(int patientId, PatientDto patient)
        {
            return _patientRepository.UpdateAsync(patientId, patient);
        }

        public Task DeletePatientAsync(int patientId)
        {
            return _patientRepository.DeleteAsync(patientId);
        }
    }

}
