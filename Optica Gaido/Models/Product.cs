using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class Product
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un nombre")]
    [Display(Name = "Nombre")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debes ingresar un nombre de menos de 100 caracteres")]
    public string Name { get; set; } = null!;

    [Display(Name = "Stock")]
    [Range(0, 10000, ErrorMessage = "Debes ingresar un stock entre 0 y 10.000")]
    public int? Stock { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
