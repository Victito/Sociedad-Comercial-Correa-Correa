using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public int IdEmpresa { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ICollection<Sesione> Sesiones { get; set; } = new List<Sesione>();
}
