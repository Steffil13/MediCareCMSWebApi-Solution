using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public decimal? DoctorFee { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
