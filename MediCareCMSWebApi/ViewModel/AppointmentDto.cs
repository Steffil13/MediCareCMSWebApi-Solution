using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCareCMSWebApi.ViewModel
{
    public class AppointmentDto
    {
        
        public int? AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? AppointmentNumber { get; set; }
        public string? PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; } = null!;
        public int TokenNumber { get; set; }
        public bool? IsConsulted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ReceptionistId { get; set; }
        public string? Notes { get; set; }
    }

}
