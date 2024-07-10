using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Transaction
{
    public int Id { get; set; }

    public string TransationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int Amonut { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
