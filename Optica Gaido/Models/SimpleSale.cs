using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Optica_Gaido.Models;

public partial class SimpleSale
{
    [Key]
    public long ID { get; set; }

    [Display(Name = "Cliente")]
    [AllowNull]
    public long? ClientID { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<SimpleSalePaymentMethod> PaymentMethods { get; set; } = new List<SimpleSalePaymentMethod>();

    public virtual ICollection<SimpleSaleProduct> Products { get; set; } = new List<SimpleSaleProduct>();
}