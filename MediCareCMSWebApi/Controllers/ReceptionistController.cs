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
        //private readonly IAppointmentService _appointmentService;
        private readonly IBillingService _billingService;

        public ReceptionistController(
            IPatientService patientService,
            //IAppointmentService appointmentService,
            IBillingService billingService)
        {
            _patientService = patientService;
            //_appointmentService = appointmentService;
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

        //[HttpPost("appointments")]
        //public async Task<ActionResult> ScheduleAppointment([FromBody] AppointmentDto appointment)
        //{
        //    var appointmentId = await _appointmentService.ScheduleAppointmentAsync(appointment);
        //    return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentId }, new { AppointmentId = appointmentId });
        //}

        //[HttpPut("appointments/{id}")]
        //public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointment)
        //{
        //    var updated = await _appointmentService.UpdateAppointmentAsync(id, appointment);
        //    if (!updated)
        //        return NotFound();
        //    return NoContent();
        //}

        //[HttpDelete("appointments/{id}")]
        //public async Task<IActionResult> CancelAppointment(int id)
        //{
        //    var canceled = await _appointmentService.CancelAppointmentAsync(id);
        //    if (!canceled)
        //        return NotFound();
        //    return NoContent();
        //}

        //[HttpGet("appointments/{id}")]
        //public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
        //{
        //    var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        //    if (appointment == null)
        //        return NotFound();
        //    return Ok(appointment);
        //}

        //[HttpGet("appointments")]
        //public async Task<ActionResult<List<AppointmentDto>>> ListAppointments(
        //    [FromQuery] DateTime? startDate,
        //    [FromQuery] DateTime? endDate,
        //    [FromQuery] int? doctorId,
        //    [FromQuery] int? patientId)
        //{
        //    var appointments = await _appointmentService.ListAppointmentsAsync(startDate, endDate, doctorId, patientId);
        //    return Ok(appointments);
        //}

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
