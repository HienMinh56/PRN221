using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? CateId { get; set; }

    public int Price { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Image { get; set; }

    public int Quantity { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
