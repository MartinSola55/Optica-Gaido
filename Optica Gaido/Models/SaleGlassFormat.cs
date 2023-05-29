using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class SaleGlassFormat
{
    [Required]
    public long SaleID { get; set; }

    [Required]
    public long GlassFormatID { get; set; }

    public virtual GlassFormat GlassFormat { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}