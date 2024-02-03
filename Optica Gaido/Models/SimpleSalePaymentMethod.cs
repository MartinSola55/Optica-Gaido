using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class SimpleSalePaymentMethod
{
    [Required]
    public long SimpleSaleID { get; set; }

    [Required]
    public short PaymentMethodID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Monto")]
    [Range(0, 1000000, ErrorMessage = "Debes ingresar un monto entre $0 y $1.000.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal Amount { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
