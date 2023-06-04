using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitado")]
    [Display(Name = "Habilitado")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

    //public virtual ICollection<SalePaymentMethod> SalePaymentMethods { get; set; } = new List<SalePaymentMethod>();
}
