using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SociedadCorreaCorrea.Commands;
using SociedadCorreaCorrea.Data;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ModelsCustoms;
using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.Views;
using System.Windows.Input;
using iText.Commons.Actions.Contexts;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using PdfSharpCore.Pdf.Filters;
using System.Windows.Media;
using System.Windows.Forms;

namespace SociedadCorreaCorrea.ViewsModels
{

    internal class MainMenuViewModel : BaseViewModel
    {
        private readonly ContextoSMMS _contexto;
        private readonly MetroWindow _window;
        public ObservableCollection<TareasDiaria> ListaTareaDiaria { get; set; } = new ObservableCollection<TareasDiaria>();
        public ObservableCollection<Factura> ListaFacturas { get; set; } = new ObservableCollection<Factura>();

        public MainMenuViewModel(MetroWindow window)
        {
            _window = window;
            _contexto = new ContextoSMMS(new DbContextOptions<ContextoSMMS>());
            CargarFacturas();
            CargarTareasDiarias();

        }


private async void CargarFacturas()
{
    using (var contexto = new ContextoSMMS())
    {
        // Obtener todas las facturas con las relaciones necesarias
        var facturas = await contexto.Facturas
            .Where(f => f.IdEmpresa == GlobalSettings.IdEmpresa)  // Filtrar por la empresa global
            .Where(f => f.Estado == "Pendiente")  // Filtrar solo las facturas con estado "Pendiente"
            .Where(f => f.FechaVencimiento >= DateOnly.FromDateTime(DateTime.Today))  // Filtrar por fecha de vencimiento mayor o igual a hoy
            .ToListAsync();

        // Limpiar la lista antes de agregar las nuevas facturas
        ListaFacturas.Clear();

        // Agregar las facturas a la ObservableCollection
        foreach (var factura in facturas)
        {
            // Verificamos si tiene una FechaVencimiento
            if (factura.FechaVencimiento.HasValue)
            {
                // Calcular la diferencia de días entre la fecha actual y la fecha de vencimiento
                var diferenciaDias = (factura.FechaVencimiento.Value.ToDateTime(new TimeOnly(0, 0))
                                     - DateOnly.FromDateTime(DateTime.Today).ToDateTime(new TimeOnly(0, 0))).Days;

                // Asignar un color basado en la diferencia de días
                if (diferenciaDias == 0)
                {
                    // Fecha de vencimiento hoy -> Rojo
                    factura.ColorVencimiento = "Red";
                }
                else if (diferenciaDias <= 4)
                {
                    // Fecha de vencimiento en 4 días -> Amarillo
                    factura.ColorVencimiento = "Yellow";
                }
                        else if (diferenciaDias <= 7)
                        {
                            // Fecha de vencimiento en 7 días -> Verde Claro
                            factura.ColorVencimiento = "#90EE90";  // Light Green
                        }
                        else
                {
                    // Más de 7 días -> Sin cambio
                    factura.ColorVencimiento = "Transparent";
                }
            }
            else
            {
                // Si no tiene fecha de vencimiento asignar color transparente
                factura.ColorVencimiento = "Transparent";
            }

            // Agregar la factura a la lista
            ListaFacturas.Add(factura);
        }

    }
}



        private void CargarTareasDiarias()
        {
            using (var contexto = new ContextoSMMS())
            {
                // Obtiene todas las tareas diarias con las relaciones necesarias
                var tareas = contexto.TareasDiarias
                                     .Include(t => t.Empleado)
                                     .Include(t => t.Empresa)
                                     .Include(t => t.Sucursal)
                                     .Where(t => t.FechaTarea == DateOnly.FromDateTime(DateTime.Today))
                                     .Where(t => t.EmpresaId == GlobalSettings.IdEmpresa) // Filtro adicional
                                     .ToList();

                // Limpia la colección antes de llenarla
                ListaTareaDiaria.Clear();

                // Verifica si hay tareas
                if (tareas.Count > 0)
                {
                    foreach (var tarea in tareas)
                    {
                        ListaTareaDiaria.Add(tarea);
                    }
                }
                else
                {
                    // Si no hay tareas, agrega un elemento especial indicando que no hay tareas
                    ListaTareaDiaria.Add(new TareasDiaria
                    {
                        NombreTarea = "Ninguna tarea asignada para el día de hoy.",
                        DescripcionTarea = string.Empty
                    });
                }
            }
        }





























































    }
}
