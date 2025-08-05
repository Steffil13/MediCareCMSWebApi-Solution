namespace MediCareCMSWebApi.ViewModel
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string? RegisterNumber { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? BloodGroup { get; set; }
        public string? Gender { get; set; }
        public string? Contact { get; set; }
        public string? EmergencyNumber { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}
