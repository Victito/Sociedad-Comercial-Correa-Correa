using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly? Fecha { get; set; }

    public int IdEmpleado { get; set; }

    public int? IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
