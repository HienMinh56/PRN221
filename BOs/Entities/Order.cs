using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string OrderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public string StoreId { get; set; } = null!;

    public string TransationId { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; } = null!;

    public virtual Transaction Transation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
