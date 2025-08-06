using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class Prescription
{
    

    public int PrescriptionId { get; set; }
   // public int? DoctorId { get; set; }

    public int AppointmentId { get; set; }

    public string? Symptoms { get; set; }

    public string? Diagnosis { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual ICollection<LabBill> LabBills { get; set; } = new List<LabBill>();

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual ICollection<PharmacyBill> PharmacyBills { get; set; } = new List<PharmacyBill>();

    public virtual ICollection<PrescribedLabTest> PrescribedLabTests { get; set; } = new List<PrescribedLabTest>();

    public virtual ICollection<PrescribedMedicine> PrescribedMedicines { get; set; } = new List<PrescribedMedicine>();
}
