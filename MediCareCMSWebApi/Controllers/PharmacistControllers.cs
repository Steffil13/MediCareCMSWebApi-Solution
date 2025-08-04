using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistControllers : ControllerBase
    {
        private readonly IPharmacistService _pharmacistService;

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
            return Ok("Medicine added successfully.");
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

        #region GeneratePharmacyBill

        [HttpPost("bill")]
        public async Task<IActionResult> GeneratePharmacyBill([FromBody] PharmacyBillViewModel model)
        {
            await _pharmacistService.GenerateBillAsync(model);
            return Ok("Pharmacy bill generated successfully.");
        }

        #endregion
    }
}
