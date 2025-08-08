using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using System.Security.Claims;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;

        public DoctorController(IDoctorService doctorService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
        }

        // 🔹 1. Get Appointments (Today & Tomorrow)
        //[HttpGet("appointments")]
        //public async Task<IActionResult> GetDoctorAppointments()
        //{
        //    var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        //    if (!int.TryParse(roleClaim, out int roleId))
        //        return Unauthorized("Invalid role in token.");

        //    int? doctorId = await _doctorService.GetDoctorIdByRoleIdAsync(roleId);
        //    if (doctorId == null)
        //        return NotFound("Doctor not found for this role.");

        //    var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId.Value);
        //    return Ok(appointments);
        //}

        [HttpGet("appointments/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);
            return Ok(appointments);
        }

        // 🔹 2. Create Prescription (Symptoms, Diagnosis, Notes)
        [HttpPost("add-prescription")]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionDto dto)
        {
            var result = await _doctorService.CreatePrescriptionAsync(dto);
            if (result == null)
                return BadRequest(new { message = "Failed to create prescription" });

            return Ok(new { message = "Prescription created successfully", data = result });
        }


        // 🔹 3. Add Prescribed Medicine
        [HttpPost("add-medicine")]
        public async Task<IActionResult> AddPrescribedMedicine([FromBody] PrescribedMedicineDto dto)
        {
            var result = await _doctorService.AddPrescribedMedicineAsync(dto);
            return result
                ? Ok(new { message = "Medicine added to prescription" })
                : BadRequest(new { message = "Failed to add medicine" });
        }

        // 🔹 4. Add Lab Test to Prescription
        [HttpPost("add-labtest")]
        public async Task<IActionResult> AddPrescribedLabTest([FromBody] PrescribedLabTestDto dto)
        {
            var result = await _doctorService.AddLabTestRequestAsync(dto);
            return result
                ? Ok(new { message = "Lab test added to prescription" })
                : BadRequest(new { message = "Failed to add lab test" });
        }

        // 🔹 5. View Prescription for Appointment
        [HttpGet("prescription/{appointmentId}")]
        public async Task<IActionResult> GetPrescriptionByAppointmentId(string appointmentNumber)
        {
            var prescription = await _doctorService.GetPrescriptionByAppointmentNumberAsync(appointmentNumber);
            return prescription != null
                ? Ok(prescription)
                : NotFound(new { message = "No prescription found for this appointment" });
        }
    }
}
