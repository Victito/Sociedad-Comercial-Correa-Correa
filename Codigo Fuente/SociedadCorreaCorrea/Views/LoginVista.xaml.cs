﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prueba.Vista
{
    /// <summary>
    /// Lógica de interacción para LoginVista.xaml
    /// </summary>
    public partial class LoginVista : MetroWindow
    {
        public LoginVista()
        {
            InitializeComponent();
        }
        private void Ventana_MouseBajo(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtusuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}