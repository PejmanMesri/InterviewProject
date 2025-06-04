using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterviewProject.Entities;

public partial class PurchaseCommissionCustomer
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public string Price { get; set; } = null!;

    public string TotalPrice { get; set; } = null!;
    [NotMapped]
    public string PersianDeliveryDate { get; set; } = "";
    public DateTime? DeliveryDate { get; set; }
    public string? PurchaseCondition { get; set; }

    public string? ProducerName { get; set; }
    [NotMapped]
    public string PersianLastPurchaseDate { get; set; } = "";
    public DateTime? LastPurchaseDate { get; set; }

    public Guid PurchaseCommissionId { get; set; }

    public Guid SellerId { get; set; }

    public virtual PurchaseCommission PurchaseCommission { get; set; } = null!;

    public virtual Customer Seller { get; set; } = null!;
}
