using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class ConsultationBill
{
    public int BillId { get; set; }

    public string? BillNumber { get; set; }

    public DateTime? DateTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int? ReceptionistId { get; set; }

    public int? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Receptionist? Receptionist { get; set; }
}
