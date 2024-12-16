﻿using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public int EmpresaId { get; set; }

    public string NombreServicio { get; set; } = null!;

    public decimal CostoServicio { get; set; }

    public string EmpresaServicio { get; set; } = null!;

    public DateOnly? FechaContratacion { get; set; }

    public DateOnly? FechaPago { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;
}