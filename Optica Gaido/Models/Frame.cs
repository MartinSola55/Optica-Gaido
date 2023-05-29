using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class Frame
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un modelo")]
    [Display(Name = "Modelo")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Debes ingresar un modelo de menos de 30 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese un modelo válido")]
    public string Model { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar una marca")]
    [Display(Name = "Marca")]
    public long BrandID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un material")]
    [Display(Name = "Material")]
    public long MaterialID { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
