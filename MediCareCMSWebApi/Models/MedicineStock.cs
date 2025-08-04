using System;
using System.Collections.Generic;

namespace MediCareCMSWebApi.Models;

public partial class MedicineStock
{
    public int MedicineStockId { get; set; }

    public int? ReorderLevel { get; set; }

    public int? Purchase { get; set; }

    public int? Issuance { get; set; }

    public int MedicineId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual MedicineInventory Medicine { get; set; } = null!;
}
