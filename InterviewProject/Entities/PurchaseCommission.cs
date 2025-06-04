using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InterviewProject.Entities;

public partial class PurchaseCommission
{
    public Guid Id { get; set; }
    //[PersianDateValidation]
    public DateTime CurrentDate { get; set; }
    [NotMapped]
    public string PersianCurrentDate { get; set; } = "";

    public int ProductCode { get; set; }

    public string ProductDescription { get; set; } = null!;

    public Guid OrderId { get; set; }

    public int Quantity { get; set; }


    //[PersianDateValidation] 
    public DateTime RequiredDate { get; set; }

    // This property will be used to bind the Persian date string
    [NotMapped]
    public string PersianRequiredDate { get; set; } = "";

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<PurchaseCommissionCustomer> PurchaseCommissionCustomers { get; set; } = new List<PurchaseCommissionCustomer>();
}
