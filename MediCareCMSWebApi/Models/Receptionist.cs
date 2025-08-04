using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class Receptionist
{
    public int ReceptionistId { get; set; }

    public string? EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public string? Addresss { get; set; }

    public DateTime? Dob { get; set; }

    public string? Email { get; set; }

    public string? BloodGroup { get; set; }

    public string? Contact { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<ConsultationBill> ConsultationBills { get; set; } = new List<ConsultationBill>();

    public virtual Role? Role { get; set; }
}
