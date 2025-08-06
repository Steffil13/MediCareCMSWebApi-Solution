namespace MediCareCMSWebApi.ViewModel
{
    public class AppointmentViewModel
    {
        public string AppointmentNumber { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; }

        public int TokenNumber { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public int ReceptionistId { get; set; }

        public string? Notes { get; set; }

        public bool IsConsulted { get; set; } = false;
    }

}
