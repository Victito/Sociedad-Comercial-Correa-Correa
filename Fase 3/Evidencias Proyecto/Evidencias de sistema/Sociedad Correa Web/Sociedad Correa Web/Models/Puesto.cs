using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Puesto
{
    public int IdPuestos { get; set; }

    public int IdEmpresa { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public string EstadoPuesto { get; set; } = null!;
}
