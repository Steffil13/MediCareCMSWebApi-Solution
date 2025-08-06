namespace MediCareCMSWebApi.ViewModel
{
    public class BillingDto
    {
        public int BillId { get; set; }
        public string? BillNumber { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? ReceptionistId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
