using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public virtual ICollection<Configuracion> Configuracions { get; set; } = new List<Configuracion>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<EntradaSalidum> EntradaSalida { get; set; } = new List<EntradaSalidum>();

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();

    public virtual ICollection<TareasDiaria> TareasDiaria { get; set; } = new List<TareasDiaria>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
