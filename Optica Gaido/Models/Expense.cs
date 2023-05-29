using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class Expense
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una descripción")]
    [Display(Name = "Descripción")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debes ingresar una descripción de menos de 100 caracteres")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Monto")]
    [Range(1, 100000, ErrorMessage = "Debes ingresar un monto entre $1 y $100.000")]
    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}
