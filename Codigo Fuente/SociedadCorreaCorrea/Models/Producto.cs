using System;
using System.Collections.Generic;

namespace SociedadCorreaCorrea.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string CodigoProducto { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string NSerie { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int Descuento { get; set; }

    public decimal Total { get; set; }

    public int IdFactura { get; set; }

    public int? IdSucursal { get; set; }

    public int IdEmpresa { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

}
