using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class HorarioEmpleado
{
    public int IdHorario { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdSucursal { get; set; }

    public int? IdEmpresa { get; set; }

    public string? DiaSemana { get; set; }

    public string? HoraEntrada { get; set; }

    public string? HoraSalida { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
