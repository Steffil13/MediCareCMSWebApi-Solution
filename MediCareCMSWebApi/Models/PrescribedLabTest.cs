using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class PrescribedLabTest
{
    public int PlabTestId { get; set; }

    public int PrescriptionId { get; set; }

    public int LabId { get; set; }

    public bool? IsCompleted { get; set; }

    public virtual LabInventory Lab { get; set; } = null!;

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual Prescription Prescription { get; set; } = null!;

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
