using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string CateId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
