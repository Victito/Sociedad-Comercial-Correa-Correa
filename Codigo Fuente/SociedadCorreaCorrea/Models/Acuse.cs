using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Acuse
{
    public int IdAcuse { get; set; }

    public string Nombre { get; set; } = null!;

    public string Recinto { get; set; } = null!;

    public DateOnly? Fecha { get; set; }

    public string Rut { get; set; } = null!;

    public string? Firma { get; set; }

    public int IdFactura { get; set; }

    public int? IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public int? NumeroFactura { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
