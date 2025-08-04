using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("appointments")]
        public async Task<IActionResult> GetDoctorAppointments()
        {
            var doctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);
            return Ok(appointments);
        }

        // 🔹 2. Create Prescription (Symptoms, Diagnosis, Notes)
        [HttpPost("add-prescription")]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionDto dto)
        {
            var result = await _doctorService.CreatePrescriptionAsync(dto);
            return result
                ? Ok(new { message = "Prescription created successfully" })
                : BadRequest(new { message = "Failed to create prescription" });
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
            var result = await _doctorService.AddPrescribedLabTestAsync(dto);
            return result
                ? Ok(new { message = "Lab test added to prescription" })
                : BadRequest(new { message = "Failed to add lab test" });
        }

        // 🔹 5. View Prescription for Appointment
        [HttpGet("prescription/{appointmentId}")]
        public async Task<IActionResult> GetPrescriptionByAppointmentId(int appointmentId)
        {
            var prescription = await _doctorService.GetPrescriptionByAppointmentIdAsync(appointmentId);
            return prescription != null
                ? Ok(prescription)
                : NotFound(new { message = "No prescription found for this appointment" });
        }
    }
