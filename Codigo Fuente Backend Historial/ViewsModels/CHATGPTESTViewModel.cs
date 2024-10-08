using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.Data;
using System.Collections.ObjectModel;
using System.Windows;
using SociedadCorreaCorrea.ViewModels;
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Commands;

namespace SociedadCorreaCorrea.ViewModels
{
    internal class CHATGPTESTViewModel : BaseViewModel
    {
        // Propiedad para manejar la colección de productos
        public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();



        // Constructor
        public CHATGPTESTViewModel()
        {
            // Inicializa la colección de productos
            Productos = new ObservableCollection<Producto>();

        }

        // Método para agregar productos a la lista
        public void AgregarProducto(string codigo, string descripcion, string nSerie, int cantidad, decimal precioUnitario, int descuento, decimal total)
        {
            var producto = new Producto
            {
                CodigoProducto = codigo,
                Descripcion = descripcion,
                NSerie = nSerie,
                Cantidad = cantidad,
                Descuento = descuento,
                PrecioUnitario = precioUnitario,
                Total = total
            };

            // Agrega el producto a la colección
            Productos.Add(producto);

        }

        public void EliminarProducto(Producto producto)
        {
            if (Productos.Contains(producto))
            {
                Productos.Remove(producto);
            }
        }

        // Método para vaciar toda la lista de productos
        public void VaciarProductos()
        {
            Productos.Clear();
        }

    }
}
