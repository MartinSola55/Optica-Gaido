using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class PaymentMethod
{
    [Key]
    public short ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un nombre")]
    [Display(Name = "Método de pago")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Debes ingresar un nombre de menos de 30 caracteres")]
    public string Name { get; set; } = null!;

    public virtual ICollection<SalePaymentMethod> SalePaymentMethods { get; set; } = new List<SalePaymentMethod>();
}
