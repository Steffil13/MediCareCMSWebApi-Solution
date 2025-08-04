using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Service;
using MediCareCMSWebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MediCareCMSWebApi.ViewModel.LabTechnicianViewModels;

namespace MediCareCMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTechnicianControllers : ControllerBase
    {
        private readonly ILabTechnicianService _labService;

        public LabTechnicianControllers(ILabTechnicianService labService)
        {
            _labService = labService;
        }

        [HttpPost("add-labtest")]
        public async Task<IActionResult> AddLabTestAsync([FromBody] LabTechnicianViewModels.AddLabTestViewModel labTest)
        {
            var result = await _labService.AddLabTestAsync(labTest);
            return Ok(new { Message = "Lab test added successfully.", LabId = result });
        }


        #region View All Lab Tests
        [HttpGet("get-all-labtests")]
        public async Task<IActionResult> GetAllLabTestsAsync()
        {
            var tests = await _labService.GetAllLabTestsAsync();
            return Ok(tests);
        }
        #endregion

        #region Get Lab Test By ID
        [HttpGet("get-labtest/{id}")]
        public async Task<IActionResult> GetLabTestByIdAsync(int id)
        {
            var test = await _labService.GetLabTestByIdAsync(id);
            if (test == null) return NotFound("Lab test not found.");
            return Ok(test);
        }
        #endregion


        #region Assign Lab Test to Patient
        
        [HttpPost("assign-labtest")]
        public async Task<IActionResult> AssignLabTestAsync([FromBody] AssignLabTestViewModel model)
        {
            await _labService.AssignLabTestAsync(model);
            return Ok(new { Message = "Lab test assigned and result recorded successfully." });
        }


        #endregion



        #region View Patient Lab History
        [HttpGet("patient-history/{patientId}")]
        public async Task<IActionResult> GetPatientLabHistoryAsync(int patientId)
        {
            var history = await _labService.GetPatientLabHistoryAsync(patientId);
            return Ok(history);
        }
        #endregion

        #region View All Lab Records
        
        [HttpGet("view-all-test-results")]
        public async Task<IActionResult> GetAllTestResults()
        {
            var results = await _labService.GetAllTestResultsAsync();
            return Ok(results);
        }

        #endregion
    }
}

