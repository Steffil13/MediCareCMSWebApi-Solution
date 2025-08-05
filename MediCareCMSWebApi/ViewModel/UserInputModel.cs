using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MediCareCMSWebApi.ViewModel
{
    public class UserInputModel
    {
            public int UserId { get; set; }  // Optional for insert/update tracking

            public string? EmployeeId { get; set; }  // Auto-generated, usually null on insert

            [StringLength(50)]
            public string? Username { get; set; } = null!;

            [StringLength(100)]
            [DataType(DataType.Password)]
            public string? Password { get; set; } = null!;
            public string? RoleName { get; set; } = null!;

            public string FullName => $"{FirstName} {LastName}".Trim();

            public bool? IsActive { get; set; }

            // Personal details common to staff types
            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            public string? Gender { get; set; }

            public string? Addresss { get; set; }

            public DateTime? Dob { get; set; }

            public string? Email { get; set; }

            public string? BloodGroup { get; set; }

            public string? Contact { get; set; }

            public int RoleId { get; set; }

            public int? DepartmentId { get; set; }

            public bool? IsActiveNullable { get; set; }  // Optional alternative IsActive        

            public List<SelectListItem>? RoleList { get; set; }
            public List<SelectListItem>? DepartmentList { get; set; }
    }
}

