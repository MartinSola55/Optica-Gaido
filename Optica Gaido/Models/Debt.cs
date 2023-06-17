using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class Debt
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un proveedor")]
    [Display(Name = "Proveedor")]
    public long ProviderID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una descripción")]
    [Display(Name = "Descripción")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Debes ingresar una descripción de menos de 500 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese una descripción válido")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Monto")]
    [Range(0, 10000000, ErrorMessage = "Debes ingresar un monto entre $0 y $10.000.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    [Display(Name = "Eliminado")]
    public DateTime? DeletedAt { get; set; }

    public virtual Provider Provider { get; set; } = null!;

    public virtual ICollection<DebtPayment> DebtPayment { get; set; } = new List<DebtPayment>();
}
