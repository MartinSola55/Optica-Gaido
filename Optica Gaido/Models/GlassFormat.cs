using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Optica_Gaido.Models;

public enum Eye
{
    Izquierdo = 1,
    Derecho = 2
}

public enum Distance
{
    Cerca = 1,
    Lejos = 2
}

public partial class GlassFormat
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una distancia")]
    public Distance Distance { get; set; }

    [Required(ErrorMessage = "Debes ingresar un ojo")]
    public Eye Eye { get; set; }

    [Required(ErrorMessage = "Debes ingresar un valor")]
    [Precision(18,2)]
    public decimal Esferic { get; set; }

    [Required(ErrorMessage = "Debes ingresar un valor")]
    [Precision(18, 2)]
    public decimal Cilindric { get; set; }

    [Required(ErrorMessage = "Debes ingresar un eje")]
    [Precision(18, 2)]
    public decimal Axis { get; set; }

    [Display(Name = "Eliminado")]
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
