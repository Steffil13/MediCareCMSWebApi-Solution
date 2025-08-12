using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Controllers
{
    [ApiController]
    [Route("api/receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IBillingService _billingService;
        private readonly MediCareDbContext _context;

        public ReceptionistController(
            IPatientService patientService,
            IAppointmentService appointmentService,
            IBillingService billingService,
            MediCareDbContext context)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _billingService = billingService;
            _context = context;
        }

        // Patient Management

        #region View All patients
        [HttpGet("all-patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }
        #endregion

        // GET api/receptionist/patients/search?registerNumber=12345
        [HttpGet("patients/search")]
        public async Task<IActionResult> SearchPatientsByRegisterNumber([FromQuery] string registerNumber)
        {
            if (string.IsNullOrWhiteSpace(registerNumber))
            {
                return BadRequest("Register number is required.");
            }

            var patients = await _context.Patients
                .Where(p => p.RegisterNumber.Contains(registerNumber))
                .ToListAsync();

            if (patients == null || patients.Count == 0)
            {
                return NotFound("No patients found with this register number.");
            }

            return Ok(patients);
        }

        // GET: api/receptionist/doctors/by-department/5
        [HttpGet("doctors/by-department/{departmentId}")]
        public async Task<IActionResult> GetDoctorsByDepartment(int departmentId)
        {
            if (departmentId <= 0)
            {
                return BadRequest("Invalid department ID.");
            }

            var doctors = await _context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new
                {
                    doctorId = d.DoctorId,
                    doctorName = d.FirstName,
                    departmentId = d.DepartmentId
                })
                .ToListAsync();

            if (doctors == null || doctors.Count == 0)
            {
                return NotFound("No doctors found for this department.");
            }

            return Ok(doctors);
        }

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
                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDto.AppointmentDate.Date,       // Strip time if needed
                    AppointmentTime = !string.IsNullOrWhiteSpace(appointmentDto.AppointmentTime)
                        ? appointmentDto.AppointmentTime
                        : throw new ArgumentException("AppointmentTime is required."),
                    TokenNumber = appointmentDto.TokenNumber,
                    PatientId = appointmentDto.PatientId,
                    DoctorId = appointmentDto.DoctorId,
                    ReceptionistId = appointmentDto.ReceptionistId,
                    AppointmentNumber = appointmentDto.AppointmentNumber
                };


                var appointmentId = await _appointmentService.ScheduleAppointmentAsync(appointment);
                var createdAppointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);

                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentId }, createdAppointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine(ex);
                return StatusCode(500, "An unexpected error occurred.");
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
