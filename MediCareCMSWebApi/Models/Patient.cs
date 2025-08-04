using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string? RegisterNumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? BloodGroup { get; set; }

    public string? Gender { get; set; }

    public string? Contact { get; set; }

    public string? EmergencyNumber { get; set; }

    public string? Address { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ConsultationBill> ConsultationBills { get; set; } = new List<ConsultationBill>();

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();
}
