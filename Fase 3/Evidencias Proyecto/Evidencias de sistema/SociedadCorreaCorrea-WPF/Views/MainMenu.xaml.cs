﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls; // Para usar controles de WPF como Button, TextBox, etc.
using MahApps.Metro.Controls;
using SociedadCorreaCorrea.Models;
using SociedadCorreaCorrea.ViewModels;
using MahApps.Metro.Controls.Dialogs;  
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using SociedadCorreaCorrea.ViewsModels;
using prueba.Vista;

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Lógica de interacción para la ventana del menú principal (MainMenu.xaml).
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor de la ventana MainMenu. Inicializa los componentes y establece el tamaño máximo de la ventana.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
            NombreUsuario.Text = UserSession.NombreUsuario;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.DataContext = new MainMenuViewModel(this); // Establecer el DataContext
        }

        #endregion

        #region DLL Import para Mover Ventana

        /// <summary>
        /// Importación de la función SendMessage de user32.dll para mover la ventana cuando el mouse es arrastrado.
        /// </summary>
        

        #endregion

        
        

        /// <summary>
        /// Evento que se dispara al hacer clic en la opción de "Ingresar Facturas". Abre la ventana de registro de facturas.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento del mouse.</param>
        private void IngresarFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var registroFacturas = new RegistroFacturas();
                registroFacturas.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        private void HistorialFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var historialFacturas = new HistorialFacturas();
                historialFacturas.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        private void AdministradorDeTrabajadores_ClickMouse(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Trabajadores = new Trabajadores();
                Trabajadores.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }

        private void DatosEstadisticosFacturas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var datosEstadisticos = new GraficosFacturas();
                datosEstadisticos.ShowDialog();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWind, int wMsg, int wParam, int lParam);
        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void panelControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;


        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void Drive_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Drive = new Drive();
                Drive.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Servicios_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var Servicios = new Servicios();
                Servicios.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private void Inicio_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue con el botón izquierdo del mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                // Crear y mostrar la ventana de RegistroFacturas
                var MainMenu = new MainMenu();
                MainMenu.Show();

                // Cierra la ventana de MainMenu
                this.Close();
            }
        }
        private async void CerrarSesion_Click(object sender, MouseButtonEventArgs e)
        {
            // Aquí, la ventana CerrarSesion se muestra de manera modal
            CerrarSesion ventanaCerrarSesion = new CerrarSesion();
            ventanaCerrarSesion.ShowDialog();

        }

    }
}
