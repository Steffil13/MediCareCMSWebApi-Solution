using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class PatientHistory
{
    public int HistoryId { get; set; }

    public int? PatientId { get; set; }

    public int? AppointmentId { get; set; }

    public int? PrescriptionId { get; set; }

    public int? PmedicineId { get; set; }

    public int? PlabTestId { get; set; }

    public int? TestResultId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual PrescribedLabTest? PlabTest { get; set; }

    public virtual PrescribedMedicine? Pmedicine { get; set; }

    public virtual Prescription? Prescription { get; set; }

    public virtual TestResult? TestResult { get; set; }
}
