using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public string Model { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar una marca")]
    [Display(Name = "Marca")]
    public long BrandID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un material")]
    [Display(Name = "Material")]
    public long MaterialID { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;

    [Display(Name = "Eliminado")]
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
