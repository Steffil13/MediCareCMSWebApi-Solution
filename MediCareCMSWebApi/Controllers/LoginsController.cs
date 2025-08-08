using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginsController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            IActionResult response = Unauthorized();
            User dbUser = _loginService.AuthenticateUser(username, password);

            if (dbUser != null)
            {
                // Default null in case not a doctor
                int? doctorId = null;

                // If this user is a doctor, fetch doctorId
                if (dbUser.RoleName.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                {
                    // Example: if you have a Doctors table linked by Username
                    using (var context = new MediCareDbContext())
                    {
                        doctorId = context.Doctors
                            .Where(d => d.Username == dbUser.Username) // adjust match column
                            .Select(d => d.DoctorId)
                            .FirstOrDefault();
                    }
                }

                var tokenString = GenerateJWTToken(dbUser);

                response = Ok(new
                {
                    uName = dbUser.Username,
                    roleId = dbUser.RoleId,
                    doctorId = doctorId, // include here
                    token = tokenString
                });
            }

            return response;
        }


        private object GenerateJWTToken(User dbUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, dbUser.Username),
                new Claim(ClaimTypes.Role, dbUser.RoleId.ToString()),
                new Claim(ClaimTypes.Role, dbUser.RoleName)// Assuming RoleId is like "Admin", "Doctor", etc.
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
