using System;
using System.Collections.Generic;

namespace InterviewProject.Entities;

public partial class PurchaseCommissionModel : PurchaseCommission
{
    public PurchaseCommissionModel()
    {

    }
    public PurchaseCommissionModel(PurchaseCommission item)
    {
        this.Id = item.Id;
        this.Quantity = item.Quantity;
        this.ProductCode = item.ProductCode;
        this.Description = item.Description;
        this.ProductDescription = item.ProductDescription;
        this.Comment = item.Comment;
        this.CurrentDate = item.CurrentDate;
        this.RequiredDate = item.RequiredDate;
        this.OrderId = item.OrderId;

        this.Order = item.Order;
        this.PurchaseCommissionCustomers = item.PurchaseCommissionCustomers;
    }
}
