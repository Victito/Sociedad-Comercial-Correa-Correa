using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int IdEmpresa { get; set; }

    public int? IdSucursal { get; set; }

    public long? IdUsuario { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string ApellidoEmpleado { get; set; } = null!;

    public DateOnly? FechaNacimientoEmpleado { get; set; }

    public string? DireccionEmpleado { get; set; }

    public string? TelefonoEmpleado { get; set; }

    public string? CorreoEmpleado { get; set; }

    public string? PuestoEmpleado { get; set; }

    public decimal? SalarioEmpleado { get; set; }

    public DateOnly? FechaContratacionEmpleado { get; set; }

    public string? EstatusEmpleado { get; set; }

    public int? IdTurno { get; set; }

    public string? RutEmpleado { get; set; }

    public int? IdTurnoPersonalizado { get; set; }

    public string? TareasEmpleado { get; set; }

    public virtual ICollection<EntradaSalidum> EntradaSalida { get; set; } = new List<EntradaSalidum>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual Sucursal? IdSucursalNavigation { get; set; }

    public virtual Turno? IdTurnoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<TurnoPersonalizado> TurnoPersonalizados { get; set; } = new List<TurnoPersonalizado>();
}
