using System;
using System.Collections.Generic;

namespace InterviewProject.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PurchaseCommissionCustomer> PurchaseCommissionCustomers { get; set; } = new List<PurchaseCommissionCustomer>();
}
