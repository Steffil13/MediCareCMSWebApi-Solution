using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    //public bool IsActive { get; set; } = true;
    
    public virtual Role Role { get; set; } = null!;
    public string? RoleName
    {
        get
        {
            return RoleId switch
            {
                1 => "Admin",
                2 => "Receptionist",
                3 => "Pharmacist",
                4 => "LabTechnician",
                5 => "Doctor",
                _ => "Unknown"
            };
        }
    }
}
