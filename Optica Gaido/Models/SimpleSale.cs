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

    [Required(ErrorMessage = "Debes ingresar un total")]
    [Column(TypeName = "money")]
    [Display(Name = "Precio")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    [Range(0, 1000000, ErrorMessage = "Debes ingresar un precio entre $0 y $1.000.000")]
    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<SimpleSalePaymentMethod> PaymentMethods { get; set; } = new List<SimpleSalePaymentMethod>();

    public virtual ICollection<SimpleSaleProduct> Products { get; set; } = new List<SimpleSaleProduct>();
}