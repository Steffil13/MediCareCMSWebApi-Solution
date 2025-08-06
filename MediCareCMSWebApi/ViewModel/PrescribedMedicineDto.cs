namespace MediCareCMSWebApi.ViewModel
{
    public class PrescribedMedicineDto
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string? Dosage { get; set; }
        public string? Duration { get; set; }
        public bool IsIssued { get; set; }  // since you added this column in DB
    }

}
