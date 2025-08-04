using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class MedicineInventory
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? Availability { get; set; }

    public virtual ICollection<MedicineStock> MedicineStocks { get; set; } = new List<MedicineStock>();

    public virtual ICollection<PrescribedMedicine> PrescribedMedicines { get; set; } = new List<PrescribedMedicine>();
}
