using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/LabTechnician")]
    [ApiController]
    public class LabTechnicianControllers : ControllerBase
    {
        private readonly ILabTechnicianService _labService;

        public LabTechnicianControllers(ILabTechnicianService labService)
        {
            _labService = labService;
        }

        #region Add Lab Test
        [HttpPost("add-lab-test")]
        public async Task<IActionResult> AddLabTest([FromBody] AddLabTestViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var labId = await _labService.AddLabTestAsync(model);
            return Ok(new { LabId = labId, Message = "Lab test added successfully" });
        }
        #endregion

        #region View All Lab Tests
        [HttpGet("all-lab-tests")]
        public async Task<IActionResult> GetAllLabTests([FromQuery] ViewAllLabTestsViewModel model)
        {
            var tests = await _labService.GetAllLabTestsAsync(model);
            return Ok(tests);
        }
        #endregion

        #region Get Lab Test By Id
        [HttpGet("lab-test/{id}")]
        public async Task<IActionResult> GetLabTestById(int id)
        {
            var test = await _labService.GetLabTestByIdAsync(id);
            if (test == null)
                return NotFound(new { Message = "Lab test not found" });

            return Ok(test);
        }
        #endregion

        #region Assign Lab Test to Patient
        [HttpPost("assign-lab-test")]
        public async Task<IActionResult> AssignLabTest([FromBody] AssignLabTestViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _labService.AssignLabTestAsync(model);
            return Ok(new { Message = "Lab test assigned successfully" });
        }
        #endregion

        #region View Patient Lab History
        [HttpGet("patient-lab-history/{patientId}")]
        public async Task<IActionResult> GetPatientLabHistory(int patientId)
        {
            var history = await _labService.GetPatientLabHistoryAsync(patientId);
            return Ok(history);
        }
        #endregion

        [HttpPost("generate-lab-bill")]
        public async Task<IActionResult> GenerateLabBill([FromBody] LabBillViewModel billModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBill = await _labService.GenerateLabBillAsync(billModel);

            return Ok(createdBill);
        }


        #region View All Prescribed Lab Tests
        [HttpGet("alllabtests")]
        public async Task<IActionResult> GetAllPrescribedLabTests()
        {
            var tests = await _labService.GetAllPrescribedLabTestsAsync();
            return Ok(tests);
        }
        #endregion

        #region Update Test Result
        [HttpPut("update-test-result/{id}")]
        public async Task<IActionResult> UpdateTestResult(int id, [FromBody] UpdateTestResultViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _labService.UpdateTestResultAsync(id, model);
            if (!success)
                return NotFound(new { Message = "Test result not found" });

            return Ok(new { Message = "Test result updated successfully" });
        }
        #endregion
    }
}

