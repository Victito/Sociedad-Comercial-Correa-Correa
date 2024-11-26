using System;
using System.Linq;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;

namespace SociedadCorreaCorrea.ViewModels
{
    public class ActualizarFacturasViewModel
    {
        private readonly ContextoSMMS _contexto;

        public ActualizarFacturasViewModel(ContextoSMMS contexto)
        {
            _contexto = contexto;
        }

        public void ActualizarFactura(InformacionFacturas facturaActualizada)
        {
            // Buscar la factura existente en la base de datos
            var facturaExistente = _contexto.Facturas.FirstOrDefault(f => f.IdFactura == facturaActualizada.Factura.IdFactura);

            if (facturaExistente != null)
            {
                // Actualizar las propiedades de la factura existente
                facturaExistente.IdSucursal = facturaActualizada.Factura.IdSucursal;
                facturaExistente.RazonSocial = facturaActualizada.Factura.RazonSocial;
                facturaExistente.NumeroFactura = facturaActualizada.Factura.NumeroFactura;
                facturaExistente.Total = facturaActualizada.Factura.Total;
                facturaExistente.RutVendedor = facturaActualizada.Factura.RutVendedor;
                facturaExistente.GiroVendedor = facturaActualizada.Factura.GiroVendedor;
                facturaExistente.RazonSocialVendedor = facturaActualizada.Factura.RazonSocialVendedor;
                facturaExistente.RutEmisor = facturaActualizada.Factura.RutEmisor;
                facturaExistente.Giro = facturaActualizada.Factura.Giro;
                facturaExistente.Direccion = facturaActualizada.Factura.Direccion;
                facturaExistente.Comuna = facturaActualizada.Factura.Comuna;
                facturaExistente.Ciudad = facturaActualizada.Factura.Ciudad;
                facturaExistente.EntregarEn = facturaActualizada.Factura.EntregarEn;
                facturaExistente.FechaEmision = facturaActualizada.Factura.FechaEmision;
                facturaExistente.FechaVencimiento = facturaActualizada.Factura.FechaVencimiento;
                facturaExistente.Cobrador = facturaActualizada.Factura.Cobrador;
                facturaExistente.NotaVenta = facturaActualizada.Factura.NotaVenta;
                facturaExistente.OrdenCompra = facturaActualizada.Factura.OrdenCompra;
                facturaExistente.Condiciones = facturaActualizada.Factura.Condiciones;
                facturaExistente.GuiaDespacho = facturaActualizada.Factura.GuiaDespacho;
                facturaExistente.PrecioUnitario = facturaActualizada.Factura.PrecioUnitario;
                facturaExistente.Cantidad = facturaActualizada.Factura.Cantidad;
                facturaExistente.Estado = facturaActualizada.Factura.Estado;

                // Guardar los cambios en la base de datos
                _contexto.SaveChanges();
            }
        }
    }
}
