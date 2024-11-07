using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class TurnoPersonalizado
{
    public int IdTurnoPersonalizado { get; set; }

    public int IdEmpleado { get; set; }

    public DateOnly FechaTurno { get; set; }

    public string DiaSemanaPersonalizado { get; set; } = null!;

    public TimeOnly HoraInicioTp { get; set; }

    public TimeOnly HoraFinTp { get; set; }

    public TimeOnly HoraAlmuerzoInicioTp { get; set; }

    public TimeOnly HoraAlmuerzoFinTp { get; set; }

    public string? ObservacionTp { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
