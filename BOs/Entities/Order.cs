using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string OrderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int TotalAmount { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? VoucherCode { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;

    public virtual Voucher? VoucherCodeNavigation { get; set; }
}
