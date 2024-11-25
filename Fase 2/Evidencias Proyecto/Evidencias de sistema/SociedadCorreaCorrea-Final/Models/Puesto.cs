using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Puesto
{
    public int IdPuestos { get; set; }

    public int IdEmpresa { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public string EstadoPuesto { get; set; } = null!;
}
