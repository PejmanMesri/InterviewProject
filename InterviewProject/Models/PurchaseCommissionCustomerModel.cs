using System;
using System.Collections.Generic;

namespace InterviewProject.Entities;

public partial class PurchaseCommissionCustomerModel : PurchaseCommissionCustomer
{
    public PurchaseCommissionCustomerModel()
    {

    }
    public PurchaseCommissionCustomerModel(PurchaseCommissionCustomer item)
    {
        this.Id = item.Id;
        this.SellerId = item.SellerId;
        this.PurchaseCommissionId = item.PurchaseCommissionId;
        this.Quantity = item.Quantity;
        this.TotalPrice = item.TotalPrice;
        this.Price = item.Price;
        this.LastPurchaseDate = item.LastPurchaseDate;
        this.DeliveryDate = item.DeliveryDate;
        this.PurchaseCondition = item.PurchaseCondition;
        this.ProducerName = item.ProducerName;

        this.Seller = item.Seller;
        this.PurchaseCommission = item.PurchaseCommission;
    }
}
