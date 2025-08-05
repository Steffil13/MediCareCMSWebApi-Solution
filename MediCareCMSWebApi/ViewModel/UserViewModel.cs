using System.ComponentModel.DataAnnotations;

namespace MediCareCMSWebApi.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }
    }
}

