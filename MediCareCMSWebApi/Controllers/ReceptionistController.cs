using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCareCMSWebApi.Controllers
{
    [ApiController]
    [Route("api/receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IBillingService _billingService;

        public ReceptionistController(
            IPatientService patientService,
            IAppointmentService appointmentService,
            IBillingService billingService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _billingService = billingService;
        }

        // Patient Management

        [HttpPost("patients")]
        public async Task<ActionResult> RegisterPatient([FromBody] PatientDto patient)
        {
            var id = await _patientService.RegisterPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id }, new { PatientId = id });
        }

        [HttpPut("patients/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto patient)
        {
            var updated = await _patientService.UpdatePatientAsync(id, patient);
            if (!updated)
                return NotFound();
            return Ok(patient);
        }

        [HttpGet("patients/{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        // Appointment Scheduling

        // GET: api/Receptionist/patient/{patientId}/appointments
        [HttpGet("patient/{patientId}/appointments")]
        public async Task<IActionResult> GetAppointmentsByPatient(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);

            if (appointments == null || !appointments.Any())
                return NotFound($"No appointments found for patient with ID {patientId}.");

            return Ok(appointments);
        }

        // GET: api/Receptionist/appointments/doctor/{doctorId}
        [HttpGet("appointments/doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsForDoctor(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);
            return Ok(appointments);
        }

        // GET: api/Receptionist/appointments/{id}
        [HttpGet("appointments/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        // POST: api/Receptionist/appointments
        // Schedule a new appointment with token generation handled internally
        [HttpPost("appointments")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentViewModel appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var appointmentId = await _appointmentService.ScheduleAppointmentAsync(appointmentDto);
                var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);

                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentId }, appointment);
            }
            catch (InvalidOperationException ex)
            {
                // Handle token limit or other business exceptions
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Receptionist/appointments/{id}
        [HttpPut("appointments/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
                return NotFound();

            appointment.AppointmentId = id; // if needed, set the ID

            var updated = await _appointmentService.UpdateAppointmentAsync(id, appointment);
            if (!updated)
                return StatusCode(500, "Failed to update appointment");

            var updatedAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
            return Ok(updatedAppointment);
        }

        // DELETE: api/Receptionist/appointments/{id}
        [HttpDelete("appointments/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var deleted = await _appointmentService.DeleteAppointmentAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // Consultation Billing

        [HttpPost("billings")]
        public async Task<ActionResult> GenerateBilling([FromBody] BillingDto billing)
        {
            var billingId = await _billingService.AddBillingAsync(billing);
            return CreatedAtAction(nameof(GetBillingById), new { id = billingId }, new { BillingId = billingId });
        }

        

        [HttpGet("billings/{id}")]
        public async Task<ActionResult<BillingDto>> GetBillingById(int id)
        {
            var billing = await _billingService.GetBillingByIdAsync(id);
            if (billing == null)
                return NotFound();
            return Ok(billing);
        }
    }

}
