using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Repository
{
    public interface IPatientRepository
    {
        Task<IList<PatientDto>> GetAllAsync();
        Task<PatientDto?> GetByIdAsync(int id);
        Task<int> AddAsync(PatientDto patient);
        Task<bool> UpdateAsync(int id, PatientDto patient);
        Task DeleteAsync(int id);
        IQueryable<Patient> Query();
    }
}
