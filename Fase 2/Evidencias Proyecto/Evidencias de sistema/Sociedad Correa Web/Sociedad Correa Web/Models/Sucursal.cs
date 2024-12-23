﻿using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
