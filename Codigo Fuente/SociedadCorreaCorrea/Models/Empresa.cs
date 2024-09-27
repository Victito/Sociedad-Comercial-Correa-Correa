﻿using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HorarioEmpleado> HorarioEmpleados { get; set; } = new List<HorarioEmpleado>();

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}