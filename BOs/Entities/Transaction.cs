using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public string TransactionId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int Amount { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User User { get; set; } = null!;
}
