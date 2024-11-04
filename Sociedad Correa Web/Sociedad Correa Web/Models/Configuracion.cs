using System;
using System.Collections.Generic;

namespace Sociedad_Correa_Web.Models;

public partial class Configuracion
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public int IdEmpresa { get; set; }

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
}
