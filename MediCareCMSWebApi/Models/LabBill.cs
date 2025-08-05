using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class LabBill
{
    public int LabBillId { get; set; }

    public string LabBillNumber { get; set; } = null!;

    public int PatientId { get; set; }

    public int PrescriptionId { get; set; }

    public int DoctorId { get; set; }

    public int? LabTechnicianId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? IssuedDate { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual LabTechnian? LabTechnician { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
