namespace MediCareCMSWebApi.ViewModel
{
    public class LabTechnicianViewModels
    {
        #region Add Lab Test ViewModel
        public class AddLabTestViewModel
        {
            public string? LabName { get; set; }
            public string? NormalRange { get; set; }
            public decimal? Price { get; set; }
            public bool? Availability { get; set; }
        }
        #endregion

        #region Assign Lab Test ViewModel
        public class AssignLabTestViewModel
        {
            public int PrescriptionId { get; set; }
            public int LabId { get; set; }
            public int? LabTechnicianId { get; set; }

            public string? ResultValue { get; set; }
            public bool? ResultStatus { get; set; }
            public string? Remarks { get; set; }
        }
        #endregion

       

        #region Lab Bill ViewModel
        public class LabBillViewModel
        {
            public string LabBillNumber { get; set; } = string.Empty;
            public int PatientId { get; set; }
            public int PrescriptionId { get; set; }
            public int DoctorId { get; set; }
            public int? LabTechnicianId { get; set; }
            public decimal TotalAmount { get; set; }
        }
        #endregion

        public class TestResultViewModel
        {
            public int TestResultId { get; set; }
            public string? ResultValue { get; set; }
            public bool? ResultStatus { get; set; }
            public string? Remarks { get; set; }
            public DateTime? RecordDate { get; set; }
            public DateTime? CreatedDate { get; set; }
            public int? PlabTestId { get; set; }

            // Optional: Include minimal Lab/Test details if needed
            public string? LabName { get; set; }
        }

    }
}
