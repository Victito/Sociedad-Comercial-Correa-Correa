﻿using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class EntradaSalidum
{
    public int IdRegistro { get; set; }

    public int IdEmpleado { get; set; }

    public int IdEmpresa { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public decimal? LatitudEntrada { get; set; }

    public decimal? LongitudEntrada { get; set; }

    public decimal? LatitudSalida { get; set; }

    public decimal? LongitudSalida { get; set; }
}
