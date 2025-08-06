namespace MediCareCMSWebApi.ViewModel
{
    public class PrescriptionDetailDto
    {
        public int PrescriptionId { get; set; }
        public string AppointmentNumber { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; }
        public int DoctorId { get; set; }

    }

}
