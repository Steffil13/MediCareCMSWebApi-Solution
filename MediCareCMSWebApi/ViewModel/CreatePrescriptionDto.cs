namespace MediCareCMSWebApi.ViewModel
{
    public class CreatePrescriptionDto
    {
        public int AppointmentId { get; set; }
        public int PrescriptionId { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }
    }

}
