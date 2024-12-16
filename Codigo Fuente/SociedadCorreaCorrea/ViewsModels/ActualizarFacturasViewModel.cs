using System;
using System.Collections.ObjectModel;
using System.Linq;
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using MahApps.Metro.Controls.Dialogs;
using SociedadCorreaCorrea.Views;

namespace SociedadCorreaCorrea.ViewModels
{
    public class ActualizarFacturasViewModel
    {
        private InformacionFacturas _facturaSeleccionada;
        private readonly ContextoSMMS _contexto;
        // Lista para almacenar todas las sucursales, inicializada automáticamente
        public ObservableCollection<Sucursal> ListaSucursal { get; set; } = new ObservableCollection<Sucursal>();

        // Inyección del diálogo
        private readonly MetroWindow _window;
        public ActualizarFacturasViewModel(ContextoSMMS contexto , MetroWindow window)
        {
            _window = window;
            _contexto = contexto;
        }

        private void CargarSucursales()
        {
            // Cargar todas las sucursales desde tu contexto
            var sucursales = _contexto.Sucursals.ToList();
            ListaSucursal.Clear();

            foreach (var suc in sucursales)
            {
                ListaSucursal.Add(suc);
            }
        }

        public bool ValidarRut(string rut)
        {
            // Elimina puntos y guiones del RUT
            rut = rut.Replace(".", "").Replace("-", "").ToUpper();

            // Verifica si el RUT está vacío
            if (string.IsNullOrEmpty(rut))
            {
                return false;
            }

            // Separa el número del dígito verificador
            string rutNumeros = rut.Length > 1 ? rut.Substring(0, rut.Length - 1) : "";
            char dv = rut.Length > 1 ? rut[rut.Length - 1] : '0';

            // Verifica si el dígito verificador es válido
            if (!char.IsDigit(dv) && dv != 'K')
            {
                return false;
            }

            // Calcula el dígito verificador esperado
            int suma = 0;
            int factor = 2;

            for (int i = rutNumeros.Length - 1; i >= 0; i--)
            {
                suma += (rutNumeros[i] - '0') * factor;
                factor = factor == 7 ? 2 : factor + 1;
            }

            int dvEsperado = 11 - (suma % 11);
            char dvEsperadoChar = dvEsperado == 10 ? 'K' : (dvEsperado == 11 ? '0' : (char)(dvEsperado + '0'));

            // Compara el dígito verificador calculado con el dígito verificador ingresado
            return dv == dvEsperadoChar;
        }

        public async Task ActualizarFacturaAsync(InformacionFacturas facturaActualizada)
        {
            // Validar el formato de los RUTs
            if (!ValidarRut(facturaActualizada.Factura.RutEmisor))
            {
                await _window.ShowMessageAsync("Error", $"El RUT Emisor '{facturaActualizada.Factura.RutEmisor}' no tiene un formato válido.");
                return;
            }

            if (!ValidarRut(facturaActualizada.Factura.RutVendedor))
            {
                await _window.ShowMessageAsync("Error", $"El RUT Vendedor '{facturaActualizada.Factura.RutVendedor}' no tiene un formato válido.");
                return;
            }

            try
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

                    if (facturaActualizada.Factura.FechaEmision.HasValue)
                        facturaExistente.FechaEmision = facturaActualizada.Factura.FechaEmision;

                    if (facturaActualizada.Factura.FechaVencimiento.HasValue)
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

                    // Mostrar mensaje de éxito
                    await _window.ShowMessageAsync("Éxito", "Factura actualizada correctamente.");

                    // Ejecutar solo si todo fue exitoso
                    var historialFacturas = new HistorialFacturas();
                    historialFacturas.Show();

                    _window.Close();  // Cierra la ventana actual
                }
                else
                {
                    await _window.ShowMessageAsync("Error", "No se encontró la factura para actualizar.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados
                await _window.ShowMessageAsync("Error", $"Ocurrió un error al actualizar la factura: {ex.Message}");
            }
        }
    }
}
