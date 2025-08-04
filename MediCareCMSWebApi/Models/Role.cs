using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<LabTechnian> LabTechnians { get; set; } = new List<LabTechnian>();

    public virtual ICollection<Pharmacist> Pharmacists { get; set; } = new List<Pharmacist>();

    public virtual ICollection<Receptionist> Receptionists { get; set; } = new List<Receptionist>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
