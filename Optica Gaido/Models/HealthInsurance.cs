using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class HealthInsurance
{
    [Key]
    public short ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un nombre")]
    [Display(Name = "Obra social")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Debes ingresar un nombre de menos de 30 caracteres")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ingrese un nombre válido")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitada")]
    [Display(Name = "Habilitada")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

    //public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
