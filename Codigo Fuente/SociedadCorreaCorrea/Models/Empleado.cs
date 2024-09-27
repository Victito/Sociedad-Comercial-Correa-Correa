using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int IdEmpresa { get; set; }

    public int? IdSucursal { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Puesto { get; set; }

    public decimal? Salario { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public string? Estatus { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HorarioEmpleado> HorarioEmpleados { get; set; } = new List<HorarioEmpleado>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
