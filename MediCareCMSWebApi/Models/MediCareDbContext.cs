using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Models;

public partial class MediCareDbContext : DbContext
{
    public MediCareDbContext()
    {
    }

    public MediCareDbContext(DbContextOptions<MediCareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<ConsultationBill> ConsultationBills { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public virtual DbSet<LabInventory> LabInventories { get; set; }

    public virtual DbSet<LabTechnian> LabTechnians { get; set; }

    public virtual DbSet<MedicineInventory> MedicineInventories { get; set; }

    public virtual DbSet<MedicineStock> MedicineStocks { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientHistory> PatientHistories { get; set; }

    public virtual DbSet<Pharmacist> Pharmacists { get; set; }

    public virtual DbSet<PharmacyBill> PharmacyBills { get; set; }

    public virtual DbSet<PrescribedLabTest> PrescribedLabTests { get; set; }

    public virtual DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MediCare_DB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC243338EF7");

            entity.Property(e => e.AppointmentId).ValueGeneratedNever();
            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.AppointmentNumber).HasMaxLength(50);
            entity.Property(e => e.AppointmentTime).HasMaxLength(20);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsConsulted).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__6C190EBB");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Patie__6B24EA82");

            entity.HasOne(d => d.Receptionist).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ReceptionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Recep__6D0D32F4");
        });

        modelBuilder.Entity<ConsultationBill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Consulta__11F2FC6AE38DAC04");

            entity.ToTable("ConsultationBill");

            entity.Property(e => e.BillNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.ConsultationBills)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Consultat__Appoi__74AE54BC");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ConsultationBills)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Consultat__Docto__72C60C4A");

            entity.HasOne(d => d.Patient).WithMany(p => p.ConsultationBills)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Consultat__Patie__71D1E811");

            entity.HasOne(d => d.Receptionist).WithMany(p => p.ConsultationBills)
                .HasForeignKey(d => d.ReceptionistId)
                .HasConstraintName("FK__Consultat__Recep__73BA3083");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED3E5AFE12");

            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.DoctorFee).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EBF69104F35");

            entity.HasIndex(e => e.EmployeeId, "UQ__Doctors__7AD04F1005FFDF3B").IsUnique();

            entity.Property(e => e.Addresss).HasMaxLength(100);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.DoctorFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Doctors__Departm__5812160E");

            entity.HasOne(d => d.Role).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Doctors__RoleId__571DF1D5");
        });

        modelBuilder.Entity<DoctorSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__DoctorSc__9C8A5B491C732ED3");

            entity.ToTable("DoctorSchedule");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSch__Docto__778AC167");
        });

        modelBuilder.Entity<LabInventory>(entity =>
        {
            entity.HasKey(e => e.LabId).HasName("PK__LabInven__EDBD68DA9B7634D1");

            entity.ToTable("LabInventory");

            entity.Property(e => e.LabName).HasMaxLength(100);
            entity.Property(e => e.NormalRange).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<LabTechnian>(entity =>
        {
            entity.HasKey(e => e.LabTechnianId).HasName("PK__LabTechn__DCAECBE3ADCA6683");

            entity.HasIndex(e => e.EmployeeId, "UQ__LabTechn__7AD04F10325974AA").IsUnique();

            entity.Property(e => e.Addresss).HasMaxLength(100);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.LabTechnians)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__LabTechni__RoleI__5FB337D6");
        });

        modelBuilder.Entity<MedicineInventory>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F21289033A5980C");

            entity.ToTable("MedicineInventory");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.ManufactureDate).HasColumnType("datetime");
            entity.Property(e => e.MedicineName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<MedicineStock>(entity =>
        {
            entity.HasKey(e => e.MedicineStockId).HasName("PK__Medicine__FA9527188318BAFD");

            entity.ToTable("MedicineStock");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineStocks)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineS__Medic__17F790F9");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC366A0C262BB");

            entity.HasIndex(e => e.RegisterNumber, "UQ__Patients__86F493316C0A52EC").IsUnique();

            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(15);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.EmergencyNumber).HasMaxLength(15);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.RegisterNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<PatientHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__PatientH__4D7B4ABD42C4B1D9");

            entity.ToTable("PatientHistory");

            entity.Property(e => e.PlabTestId).HasColumnName("PLabTestId");
            entity.Property(e => e.PmedicineId).HasColumnName("PMedicineId");

            entity.HasOne(d => d.Appointment).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__PatientHi__Appoi__114A936A");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__PatientHi__Patie__10566F31");

            entity.HasOne(d => d.PlabTest).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PlabTestId)
                .HasConstraintName("FK__PatientHi__PLabT__14270015");

            entity.HasOne(d => d.Pmedicine).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PmedicineId)
                .HasConstraintName("FK__PatientHi__PMedi__1332DBDC");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK__PatientHi__Presc__123EB7A3");

            entity.HasOne(d => d.TestResult).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.TestResultId)
                .HasConstraintName("FK__PatientHi__TestR__151B244E");
        });

        modelBuilder.Entity<Pharmacist>(entity =>
        {
            entity.HasKey(e => e.PharmacistId).HasName("PK__Pharmaci__5B5BDA16D8375BC3");

            entity.HasIndex(e => e.EmployeeId, "UQ__Pharmaci__7AD04F10D4E32FDF").IsUnique();

            entity.Property(e => e.Addresss).HasMaxLength(100);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Pharmacists)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Pharmacis__RoleI__5BE2A6F2");
        });

        modelBuilder.Entity<PharmacyBill>(entity =>
        {
            entity.HasKey(e => e.PharmacyId).HasName("PK__Pharmacy__BD9D2AAE226B85C8");

            entity.Property(e => e.IssuedDate).HasColumnType("datetime");
            entity.Property(e => e.PharmacyBillId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PmedicineId).HasColumnName("PMedicineId");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pharmacist).WithMany(p => p.PharmacyBills)
                .HasForeignKey(d => d.PharmacistId)
                .HasConstraintName("FK__PharmacyB__Pharm__0D7A0286");

            entity.HasOne(d => d.Pmedicine).WithMany(p => p.PharmacyBills)
                .HasForeignKey(d => d.PmedicineId)
                .HasConstraintName("FK__PharmacyB__PMedi__0B91BA14");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PharmacyBills)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK__PharmacyB__Presc__0C85DE4D");
        });

        modelBuilder.Entity<PrescribedLabTest>(entity =>
        {
            entity.HasKey(e => e.PlabTestId).HasName("PK__Prescrib__CF04C6B0860BD9D1");

            entity.Property(e => e.PlabTestId).HasColumnName("PLabTestId");
            entity.Property(e => e.IsCompleted).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Lab).WithMany(p => p.PrescribedLabTests)
                .HasForeignKey(d => d.LabId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescribe__LabId__02FC7413");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescribedLabTests)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescribe__Presc__02084FDA");
        });

        modelBuilder.Entity<PrescribedMedicine>(entity =>
        {
            entity.HasKey(e => e.PmedicineId).HasName("PK__Prescrib__F5FA712EFA34CD49");

            entity.ToTable("PrescribedMedicine");

            entity.Property(e => e.PmedicineId).HasColumnName("PMedicineId");
            entity.Property(e => e.Dosage).HasMaxLength(100);
            entity.Property(e => e.Duration).HasMaxLength(100);

            entity.HasOne(d => d.Medicine).WithMany(p => p.PrescribedMedicines)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescribe__Medic__7F2BE32F");

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescribedMedicines)
                .HasForeignKey(d => d.PrescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescribe__Presc__7E37BEF6");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130832350346E8");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Diagnosis).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Symptoms).HasMaxLength(500);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prescript__Appoi__7A672E12");
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => e.ReceptionistId).HasName("PK__Receptio__0F8C20A8FF7F6EB4");

            entity.HasIndex(e => e.EmployeeId, "UQ__Receptio__7AD04F107011C109").IsUnique();

            entity.Property(e => e.Addresss).HasMaxLength(100);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Receptionists)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Reception__RoleI__5165187F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A323E75BE");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160CEAE38B2").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__TestResu__E2463587959A918F");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlabTestId).HasColumnName("PLabTestId");
            entity.Property(e => e.RecordDate).HasColumnType("datetime");
            entity.Property(e => e.ResultStatus).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.PlabTest).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.PlabTestId)
                .HasConstraintName("FK__TestResul__PLabT__08B54D69");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C7BCD702D");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4E2D4ADD7").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
