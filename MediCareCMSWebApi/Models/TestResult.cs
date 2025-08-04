using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class TestResult
{
    public int TestResultId { get; set; }

    public string? ResultValue { get; set; }

    public bool? ResultStatus { get; set; }

    public string? Remarks { get; set; }

    public DateTime? RecordDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? PlabTestId { get; set; }

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual PrescribedLabTest? PlabTest { get; set; }
}
