using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
