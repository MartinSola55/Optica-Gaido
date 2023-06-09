﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Optica_Gaido.Models;

public partial class Sale
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un precio")]
    [Column(TypeName = "money")]
    [Display(Name = "Precio")]
    [Range(0, 1000000, ErrorMessage = "Debes ingresar un precio entre $0 y $1.000.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }

    [Display(Name = "Seña")]
    [Column(TypeName = "money")]
    [Range(1, 1000000, ErrorMessage = "Debes ingresar una seña entre $1 y $1.000.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal? Deposit { get; set; }

    [Precision(18, 2)]
    [Display(Name = "Altura de película")]
    [Range(0, 1000000, ErrorMessage = "Debes ingresar una altura entre 0 y 1.000")]
    [DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
    public decimal? MovieHeight { get; set; }

    [Required(ErrorMessage = "Debes ingresar una distancia interpelicular")]
    [Display(Name = "D.I.P")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Debes ingresar una distancia de menos de 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese una distancia válida")]
    public string Dip { get; set; } = null!;

    [Display(Name = "Observaciones")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Debes ingresar una observación de menos de 500 caracteres")]
    [AllowNull]
    public string Observation { get; set; }

    [Required(ErrorMessage = "Debes seleccionar si cuenta con anti reflex")]
    [Display(Name = "Anti reflex")]
    public bool IsAr { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un tipo de cristal")]
    [Display(Name = "Tipo de cristal")]
    public long GlassTypeID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un color")]
    [Display(Name = "Color")]
    public long GlassColorID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un foco")]
    [Display(Name = "Foco")]
    public long GlassFocusTypeID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un médico")]
    [Display(Name = "Médico")]
    public long DoctorID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un vendedor")]
    [Display(Name = "Vendedor")]
    public short SellerID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un cliente")]
    [Display(Name = "Cliente")]
    public long ClientID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar un marco")]
    [Display(Name = "Marco")]
    public long FrameID { get; set; }

    [Required(ErrorMessage = "Debes ingresar una fecha de entrega")]
    [Display(Name = "Fecha de entrega")]
    [NotMapped]
    [DateFormat]
    public string DeliveryDateString { get; set; }

    public DateTime DeliveryDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Frame Frame { get; set; } = null!;

    public virtual GlassColor GlassColor { get; set; } = null!;

    public virtual GlassType GlassType { get; set; } = null!;

    public virtual GlassFocusType GlassFocusType { get; set; } = null!;

    public virtual ICollection<SalePaymentMethod> SalePaymentMethods { get; set; } = new List<SalePaymentMethod>();

    public virtual Seller Seller { get; set; } = null!;

    public virtual ICollection<GlassFormat> GlassFormats { get; set; } = new List<GlassFormat>();
}

public class DateFormatAttribute : RegularExpressionAttribute
{
    public DateFormatAttribute() : base(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$")
    {
        ErrorMessage = "El formato de la fecha debe ser DD/MM/AAAA";
    }
}