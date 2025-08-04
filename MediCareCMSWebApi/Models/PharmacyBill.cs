using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class PharmacyBill
{
    public int PharmacyId { get; set; }

    public string? PharmacyBillId { get; set; }

    public int? PmedicineId { get; set; }

    public int? PrescriptionId { get; set; }

    public int? PharmacistId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? IssuedDate { get; set; }

    public bool? IsIssued { get; set; }

    public virtual Pharmacist? Pharmacist { get; set; }

    public virtual PrescribedMedicine? Pmedicine { get; set; }

    public virtual Prescription? Prescription { get; set; }
}
