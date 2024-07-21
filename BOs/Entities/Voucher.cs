using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Voucher
{
    public int Id { get; set; }

    public string VoucherCode { get; set; } = null!;

    public string? Description { get; set; }

    public int Discount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    public int? MinimumOrderAmount { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
