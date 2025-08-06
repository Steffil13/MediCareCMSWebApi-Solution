using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MediCareDbContext _context;
        private readonly DbSet<Patient> _dbSet;

        public PatientRepository(MediCareDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Patient>();
        }   

        public async Task<IList<PatientDto>> GetAllAsync()
        {
            var patients = await _dbSet.ToListAsync();
            // Map entities to DTOs here
            return patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                RegisterNumber = p.RegisterNumber,
                Dob = p.Dob,
                BloodGroup = p.BloodGroup,
                Gender = p.Gender,
                Contact = p.Contact,
                EmergencyNumber = p.EmergencyNumber,
                Address = p.Address,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = await _dbSet.FindAsync(id);
            if (patient == null) return null;

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                RegisterNumber = patient.RegisterNumber,
                Dob = patient.Dob,
                BloodGroup = patient.BloodGroup,
                Gender = patient.Gender,
                Contact = patient.Contact,
                EmergencyNumber = patient.EmergencyNumber,
                Address = patient.Address,
                IsActive = patient.IsActive
            };
        }

        public async Task<int> AddAsync(PatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RegisterNumber = dto.RegisterNumber,
                Dob = dto.Dob,
                BloodGroup = dto.BloodGroup,
                Gender = dto.Gender,
                Contact = dto.Contact,
                EmergencyNumber = dto.EmergencyNumber,
                Address = dto.Address,
                IsActive = dto.IsActive ?? true,
                CreatedDate = DateTime.UtcNow
            };

            await _dbSet.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient.PatientId;
        }

        public async Task<bool> UpdateAsync(int id, PatientDto dto)
        {
            var patient = await _dbSet.FindAsync(id);
            if (patient == null) return false;

            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.RegisterNumber = dto.RegisterNumber;
            patient.Dob = dto.Dob;
            patient.BloodGroup = dto.BloodGroup;
            patient.Gender = dto.Gender;
            patient.Contact = dto.Contact;
            patient.EmergencyNumber = dto.EmergencyNumber;
            patient.Address = dto.Address;
            patient.IsActive = dto.IsActive ?? patient.IsActive;

            _dbSet.Update(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _dbSet.FindAsync(id);
            if (patient != null)
            {
                _dbSet.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Patient> Query()
        {
            return _dbSet.AsQueryable();
        }
    }

}
