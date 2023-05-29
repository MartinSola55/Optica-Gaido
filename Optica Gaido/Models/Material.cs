using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public partial class Material
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una descripción")]
    [Display(Name = "Descripción")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debes ingresar una descripción de menos de 100 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese una descripción válida")]
    public string Description { get; set; } = null!;

    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
