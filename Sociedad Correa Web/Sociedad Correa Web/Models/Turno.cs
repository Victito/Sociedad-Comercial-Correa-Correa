using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public int IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public string NombreTurno { get; set; } = null!;

    public string DiaSemanaTurno { get; set; } = null!;

    public TimeOnly HoraInicioTurno { get; set; }

    public TimeOnly HoraFinTurno { get; set; }

    public TimeOnly HoraAlmuerzoInicioTurno { get; set; }

    public TimeOnly HoraAlmuerzoFinTurno { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
