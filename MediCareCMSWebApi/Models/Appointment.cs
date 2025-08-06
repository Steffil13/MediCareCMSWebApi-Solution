using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCareCMSWebApi.Models;

public partial class Appointment
{
    
    public int AppointmentId { get; set; }

    public string? AppointmentNumber { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string AppointmentTime { get; set; } = null!;

    public int TokenNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int ReceptionistId { get; set; }

    public bool? IsConsulted { get; set; }

    public virtual ICollection<ConsultationBill> ConsultationBills { get; set; } = new List<ConsultationBill>();

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Receptionist Receptionist { get; set; } = null!;
}