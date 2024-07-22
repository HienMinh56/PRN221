using System;
using System.Collections.Generic;

namespace BOs.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int Role { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
