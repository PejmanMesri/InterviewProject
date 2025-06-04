using System;
using System.Collections.Generic;

namespace InterviewProject.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public int OrderNo { get; set; }

    public virtual ICollection<PurchaseCommission> PurchaseCommissions { get; set; } = new List<PurchaseCommission>();
}
