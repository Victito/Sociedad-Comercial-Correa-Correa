using System.Diagnostics;
using System.Linq;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;

namespace SociedadCorreaCorrea.ViewModels
{
    public class ActualizarProductosViewModel
    {
        private readonly ContextoSMMS _contexto;

        public ActualizarProductosViewModel(ContextoSMMS contexto)
        {
            _contexto = contexto;
        }

        public void ActualizarProducto(Producto productoActualizado)
        {
            // Buscar el producto existente en la base de datos
            var productoExistente = _contexto.Productos.FirstOrDefault(p => p.IdProducto == productoActualizado.IdProducto);

            if (productoExistente != null)
            {
                // Actualizar las propiedades del producto existente
                productoExistente.CodigoProducto = productoActualizado.CodigoProducto;
                productoExistente.Descripcion = productoActualizado.Descripcion;
                productoExistente.NSerie = productoActualizado.NSerie;
                productoExistente.Cantidad = productoActualizado.Cantidad;
                productoExistente.PrecioUnitario = productoActualizado.PrecioUnitario;
                productoExistente.Descuento = productoActualizado.Descuento;

                // Calcular el Total teniendo en cuenta el descuento
                if (productoExistente.Descuento > 0)
                {
                    productoExistente.Total = productoExistente.PrecioUnitario * productoExistente.Cantidad * (1 - productoExistente.Descuento / 100.0m);
                }
                else
                {
                    productoExistente.Total = productoExistente.PrecioUnitario * productoExistente.Cantidad;
                }

                // Actualizar la factura asociada
                ActualizarFactura(productoExistente.IdFactura);

                // Guardar los cambios en la base de datos
                _contexto.SaveChanges();
            }
        }

        // Método para actualizar la factura basado en los productos restantes
        private void ActualizarFactura(int idFactura)
        {
            // Obtener todos los productos relacionados con la factura
            var productosRestantes = _contexto.Productos
                                                  .Where(p => p.IdFactura == idFactura)
                                                  .ToList(); // Traer a memoria los productos restantes

            // Intentar encontrar la factura correspondiente
            var factura = _contexto.Facturas.FirstOrDefault(f => f.IdFactura == idFactura);
            if (factura == null) return; // Si no se encuentra la factura, salimos

            // Actualizar el total y la cantidad basados en los productos restantes
            if (productosRestantes != null && productosRestantes.Count > 0)
            {
                factura.Total = productosRestantes.Sum(p => p.Total); // Sumar el total de los productos restantes
                factura.Cantidad = productosRestantes.Sum(p => p.Cantidad); // Sumar la cantidad de los productos restantes
                                                                            // factura.PrecioUnitario = productosRestantes.Sum(p => p.PrecioUnitario); // Ajuste en caso de que sea necesario
            }
            else
            {
                // Si no quedan productos, se actualiza a cero
                factura.Total = 0;
                factura.Cantidad = 0;
                // factura.PrecioUnitario = 0; // Asegúrate de que esto sea correcto según tu lógica
            }

            // Guardar los cambios en la base de datos
            _contexto.SaveChanges();
            Debug.WriteLine($"Factura actualizada: ID {factura.IdFactura}, Nuevo Total: {factura.Total}, Nueva Cantidad: {factura.Cantidad}");
        }
    }
}
