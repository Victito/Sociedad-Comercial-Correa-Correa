using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public long IdUsuario { get; set; }

    public int? IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public string? RazonSocial { get; set; }

    public string? RutEmisor { get; set; }

    public string? Giro { get; set; }

    public string? Direccion { get; set; }

    public string? Comuna { get; set; }

    public string? Ciudad { get; set; }

    public string? EntregarEn { get; set; }

    public DateOnly? FechaEmision { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public string? Cobrador { get; set; }

    public int? NotaVenta { get; set; }

    public string? OrdenCompra { get; set; }

    public string? Condiciones { get; set; }

    public string? GuiaDespacho { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Acuse> Acuses { get; set; } = new List<Acuse>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new HashSet<Producto>();
}
