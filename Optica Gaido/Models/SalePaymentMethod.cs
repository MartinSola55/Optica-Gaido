using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class SalePaymentMethod
{
    [Required]
    public long SaleID { get; set; }

    [Required]
    public short PaymentMethodID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Monto")]
    [Range(0, 100000, ErrorMessage = "Debes ingresar un monto entre $0 y $100.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal Amount { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    //public virtual Sale Sale { get; set; } = null!;
}
