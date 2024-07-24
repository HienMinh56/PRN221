using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOs.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    [StringLength(100, MinimumLength = 10, ErrorMessage = "Product Name must be between 10 and 100 characters long.")]
    public string Name { get; set; } = null!;

    public string? CateId { get; set; }

    [Range(10000, int.MaxValue, ErrorMessage = "Price must be >= 10.000 VND.")]
    public int Price { get; set; }

    [StringLength(100, MinimumLength = 20, ErrorMessage = "Title must be between 20 and 100 characters long.")]
    public string Title { get; set; } = null!;

    [StringLength(500, MinimumLength = 50,ErrorMessage = "Description must be between 50 and 500 characters long.")]
    public string Description { get; set; } = null!;

    public string? Image { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be >= 0.")]
    public int Quantity { get; set; }

    public int Status { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
