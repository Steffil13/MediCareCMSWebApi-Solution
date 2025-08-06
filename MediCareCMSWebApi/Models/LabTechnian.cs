using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class LabTechnian
{
    public int LabTechnianId { get; set; }

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

    public virtual ICollection<LabBill> LabBills { get; set; } = new List<LabBill>();

    public virtual Role? Role { get; set; }
}
