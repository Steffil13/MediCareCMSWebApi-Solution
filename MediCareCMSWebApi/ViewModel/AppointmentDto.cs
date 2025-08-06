namespace MediCareCMSWebApi.ViewModel
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public int TokenNumber { get; set; }
        public bool? IsConsulted { get; set; }

    }

}
