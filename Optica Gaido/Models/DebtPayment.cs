using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class DebtPayment
{
    [Key]
    public long ID { get; set; }

    [ForeignKey("Debt")]
    public long DebtID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Monto")]
    [Range(0, 10000000, ErrorMessage = "Debes ingresar un monto entre $0 y $10.000.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
