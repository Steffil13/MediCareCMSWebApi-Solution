namespace MediCareCMSWebApi.ViewModels
{
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Availability { get; set; }
    }


    public class PrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string MedicineName { get; set; } = null!;

        public DateTime DatePrescribed { get; set; }
        public List<PrescribedMedicineViewModel> Medicines { get; set; } = new();
    }

    public class PrescribedMedicineViewModel
    {
        public string MedicineName { get; set; } = null!;
        public string Dosage { get; set; } = null!;
    }


    public class PatientHistoryViewModel
    {
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int PrescriptionId { get; set; }
        public string MedicineName { get; set; } = "N/A";
        public string LabTestName { get; set; } = "N/A";
        public string TestResult { get; set; } = "Pending";
    }


    public class PharmacyBillViewModel
    {
        public int PrescriptionId { get; set; }
        public int PmedicineId { get; set; }
        public int PharmacistId { get; set; }
        public decimal TotalAmount { get; set; }
    }


    public class BillItemViewModel
    {
        public string? MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
