using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Optica_Gaido.Models;

public partial class Client
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

    [Required(ErrorMessage = "Debes ingresar una dirección")]
    [Display(Name = "Dirección")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debes ingresar una dirección de menos de 100 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s0-9.]+$", ErrorMessage = "Ingrese una dirección válida")]
    public string Adress { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar un teléfono")]
    [Display(Name = "Teléfono")]
    [StringLength(12, MinimumLength = 5, ErrorMessage = "Debes ingresar un teléfono de entre 5 y 12 caracteres")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Debes ingresar un teléfono válido")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Debes ingresar un monto")]
    [Column(TypeName = "money")]
    [Display(Name = "Cuenta corriente")]
    [Range(0, 100000, ErrorMessage = "Debes ingresar una cuenta corriente entre $0 y $100.000")]
    public decimal Debt { get; set; } = 0;

    [Display(Name = "Obra social")]
    public short? HealthInsuranceID { get; set; }

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitado")]
    [Display(Name = "Habilitado")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public virtual HealthInsurance HealthInsurance { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
