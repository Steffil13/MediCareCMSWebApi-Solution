using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        #region 1- Get All Users with Password

        [HttpGet("users")]
        public ActionResult<List<UserViewModel>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        #endregion


        #region Staff Management

        // POST: api/Admin/CreateStaff
        [HttpPost("CreateStaff")]
        public IActionResult CreateStaff([FromBody] UserInputModel userInput)
        {
            if (userInput == null)
            {
                return BadRequest("Staff details are required.");
            }

            try
            {
                var result = _userService.AddUser(userInput);

                return Ok(new
                {
                    message = "Staff created successfully.",
                    Username = result.Username,
                    Password = result.Password
                });
            }
            catch (Exception ex)
            {
                // Log exception as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion


        #region 3- Update Staff
        [HttpPut("UpdateStaff/{id}")]
        public IActionResult UpdateStaff(int id, [FromBody] UserInputModel userInput)
        {
            Console.Write("jabvhd", userInput);
            if (userInput == null)
                return BadRequest("Staff details are required.");

            var result = _userService.UpdateUser(id, userInput);
            if (!result)
                return NotFound();

            return Ok("Staff updated successfully.");
        }
        #endregion

        #region 4- Deactivate Staff
        [HttpPut("DeactivateStaff/{id}")]
        public IActionResult DeactivateStaff(int id)
        {
            var result = _userService.DeactivateUser(id);
            if (!result)
                return NotFound();

            return Ok("Staff deactivated.");
        }
        #endregion

        [HttpGet("users/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // GET: api/admin/departments
        [HttpGet("departments")]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = _userService.GetAllDepartments();
            return Ok(departments);
        }

        // POST: api/admin/departments
        [HttpPost("departments")]
        public ActionResult AddDepartment([FromBody] DepartmentInputModel input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.DepartmentName))
                return BadRequest("Department name is required.");

            var newDepartmentId = _userService.AddDepartment(input.DepartmentName, input.DoctorFee);

            return CreatedAtAction(nameof(GetAllDepartments), new { id = newDepartmentId }, new { DepartmentId = newDepartmentId });
        }
        // GET: api/admin/roles
        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
        {
            var roles = await _userService.GetAllRolesAsync();

            if (roles == null || !roles.Any())
                return NotFound("No roles found");

            return Ok(roles);
        }


    }
}
