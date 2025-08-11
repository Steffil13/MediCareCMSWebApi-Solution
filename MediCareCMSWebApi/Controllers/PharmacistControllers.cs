using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistControllers : ControllerBase
    {
        private readonly IPharmacistService _pharmacistService;
        private readonly ILogger<PharmacistControllers> _logger;

        #region Constructor

        public PharmacistControllers(IPharmacistService pharmacistService)
        {
            _pharmacistService = pharmacistService;
        }

        #endregion

        #region AddMedicine

        [HttpPost("medicine")]
        public async Task<IActionResult> AddMedicine([FromBody] MedicineViewModel model)
        {
            await _pharmacistService.AddMedicineAsync(model);
            return Ok(new { message = "Medicine added successfully." });
        }

        #endregion

        #region View All medicines
        [HttpGet("all-medicines")]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _pharmacistService.GetAllMedicinesAsync();
            return Ok(medicines);
        }
        #endregion

        #region GetMedicineById

        [HttpGet("medicine/{id}")]
        public async Task<IActionResult> GetMedicineById(int id)
        {
            var medicine = await _pharmacistService.GetMedicineByIdAsync(id);
            if (medicine == null) return NotFound("Medicine not found.");
            return Ok(medicine);
        }

        #endregion

        #region GetAllPrescriptions

        [HttpGet("prescriptions")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = await _pharmacistService.GetAllPrescriptionsAsync();
            return Ok(prescriptions);
        }

        #endregion

        #region GetPrescriptionById

        [HttpGet("prescription/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var prescription = await _pharmacistService.GetPrescriptionByIdAsync(id);
            if (prescription == null) return NotFound("Prescription not found.");
            return Ok(prescription);
        }

        #endregion

        #region GetPatientHistory

        [HttpGet("history/{patientId}")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            var history = await _pharmacistService.GetPatientHistoryAsync(patientId);
            return Ok(history);
        }

        #endregion

        #region history

        [HttpGet("history/all")]
        public async Task<IActionResult> GetAllPatientHistories()
        {
            var histories = await _pharmacistService.GetAllPatientHistoriesAsync();
            return Ok(histories);
        }



        #endregion

        [HttpPut("issue-medicine/{prescribedMedicineId}")]
        public async Task<IActionResult> IssueMedicine(int prescribedMedicineId)
        {
            var result = await _pharmacistService.IssueMedicineAsync(prescribedMedicineId);

            if (!result)
                return BadRequest("Unable to issue medicine. Either already issued or insufficient stock.");

            return Ok("Medicine issued successfully and stock updated.");
        }


        [HttpGet("bill-history")]
        public async Task<IActionResult> GetBillHistory()
        {
            var bills = await _pharmacistService.GetBillHistoryAsync();

            if (bills == null || bills.Count == 0)
                return NotFound("No bill history found.");

            return Ok(bills);
        }

        [HttpPost("bill")]
        public async Task<IActionResult> GeneratePharmacyBill([FromBody] PharmacyBillViewModel model)
        {
            try
            {
                var bill = await _pharmacistService.GenerateBillAsync(model);  // capture returned bill
                return Ok(bill);  // now you can return it
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating pharmacy bill");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("bill/by-prescribed-medicine/{pmId}")]
        public async Task<IActionResult> GetBillByPrescribedMedicineId(int pmId)
        {
            var bill = await _pharmacistService.GetBillByPrescribedMedicineIdAsync(pmId);
            if (bill == null)
                return NotFound("Bill not found.");
            return Ok(bill);
        }
    }
}
