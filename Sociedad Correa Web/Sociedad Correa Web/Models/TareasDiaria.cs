using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class TareasDiaria
{
    public int IdEmpresa { get; set; }

    public int IdEmpleado { get; set; }

    public string NombreTarea { get; set; } = null!;

    public string? DescripcionTarea { get; set; }

    public DateOnly FechaTarea { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraTermino { get; set; }
}
