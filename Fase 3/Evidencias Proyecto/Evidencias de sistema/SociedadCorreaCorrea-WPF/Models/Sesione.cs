using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Sesione
{
    public long IdSesion { get; set; }

    public long IdUsuario { get; set; }

    public string TokenSesion { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? IpOrigen { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
