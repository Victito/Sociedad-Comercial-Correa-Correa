using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class TareasDiaria
{
    public int TareaDiariaId { get; set; }

    public int EmpresaId { get; set; }

    public int EmpleadoId { get; set; }

    public string NombreTarea { get; set; } = null!;

    public string? DescripcionTarea { get; set; }

    public DateOnly FechaTarea { get; set; }

    public TimeOnly HoraInicioTarea { get; set; }

    public TimeOnly HoraTerminoTarea { get; set; }

    public int? SucursalId { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual Sucursal? Sucursal { get; set; }
}