using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class DoctorSchedule
{
    public int ScheduleId { get; set; }

    public int DoctorId { get; set; }

    public DateTime Date { get; set; }

    public bool IsAvailable { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
