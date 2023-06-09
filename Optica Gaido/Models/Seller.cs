﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class Seller
{
    [Key]
    public short ID { get; set; }

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

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitado")]
    [Display(Name = "Habilitado")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
