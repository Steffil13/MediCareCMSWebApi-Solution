using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class LabInventory
{
    public int LabId { get; set; }

    public string? LabName { get; set; }

    public string? NormalRange { get; set; }

    public decimal? Price { get; set; }

    public bool? Availability { get; set; }

    public virtual ICollection<PrescribedLabTest> PrescribedLabTests { get; set; } = new List<PrescribedLabTest>();
}
