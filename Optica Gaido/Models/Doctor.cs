using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class Doctor
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

    [Required(ErrorMessage = "Debes ingresar una matrícula")]
    [Display(Name = "Matrícula")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "Debes ingresar una matrícula de menos de 10 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s0-9]+$", ErrorMessage = "Ingrese una matrícula válida")]
    public string License { get; set; } = null!;

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitado")]
    [Display(Name = "Habilitado")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
