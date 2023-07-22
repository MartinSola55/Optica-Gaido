using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class SimpleSaleProduct
{
    [Key]
    public long ID { get; set; }

    [Required]
    public long SimpleSaleID { get; set; }

    [Required]
    public long ProductID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una cantidad")]
    [Display(Name = "Cantidad")]
    [Range(1, 1000, ErrorMessage = "Debes ingresar una cantidad entre 1 y 1000")]
    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    [Display(Name = "Precio")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal SettedPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
