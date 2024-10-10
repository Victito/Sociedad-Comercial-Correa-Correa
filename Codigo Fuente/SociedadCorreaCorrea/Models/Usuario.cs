using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public int IdEmpresa { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
