using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class Provider
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un nombre")]
    [Display(Name = "Nombre")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Debes ingresar un nombre de menos de 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese un nombre válido")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar un apellido")]
    [Display(Name = "Apellido")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Debes ingresar un apellido de menos de 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese un apellido válido")]
    public string Surname { get; set; } = null!;

    [Display(Name = "Eliminado")]
    public DateTime? DeletedAt { get; set; }
}
