using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class PrescribedMedicine
{
    public int PmedicineId { get; set; }

    public int PrescriptionId { get; set; }

    public int MedicineId { get; set; }

    public string? Dosage { get; set; }

    public string? Duration { get; set; }

    public bool IsIssued { get; set; }

    public virtual MedicineInventory Medicine { get; set; } = null!;

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual ICollection<PharmacyBill> PharmacyBills { get; set; } = new List<PharmacyBill>();

    public virtual Prescription Prescription { get; set; } = null!;
}
