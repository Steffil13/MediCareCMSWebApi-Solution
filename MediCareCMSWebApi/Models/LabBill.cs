using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCareCMSWebApi.Models
{
    public class LabBill
    {
        [Key]
        public int LabBillId { get; set; }

        [Required]
        [StringLength(50)]
        public string LabBillNumber { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public int? LabTechnicianId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime IssuedDate { get; set; } = DateTime.Now;

        public bool IsPaid { get; set; } = false;

        // Navigation properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("PrescriptionId")]
        public virtual Prescription Prescription { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("LabTechnicianId")]
        public virtual LabTechnian LabTechnician { get; set; }
    }
}
