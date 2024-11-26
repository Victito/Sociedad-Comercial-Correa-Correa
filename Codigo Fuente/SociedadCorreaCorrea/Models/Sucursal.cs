using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    public virtual ICollection<TareasDiaria> TareasDiaria { get; set; } = new List<TareasDiaria>();
}
