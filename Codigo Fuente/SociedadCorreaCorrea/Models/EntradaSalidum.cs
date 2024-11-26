using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class EntradaSalidum
{
    public int IdRegistro { get; set; }

    public int IdEmpleado { get; set; }

    public int IdEmpresa { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
