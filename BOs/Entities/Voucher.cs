using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOs.Entities;

public partial class Voucher
{
    public int Id { get; set; }

    public string VoucherCode { get; set; } = null!;

    [StringLength(100, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 100 characters long.")]
    public string? Description { get; set; }

    [Range(1, 99, ErrorMessage = "Discount must be greater than 0 and less than 100.")]
    public int Discount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Status { get; set; } = null!;

    [Range(10000, int.MaxValue, ErrorMessage = "MinimumOrderAmount must be >= 10.000 VND.")]
    public int? MinimumOrderAmount { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be >= 0.")]
    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
