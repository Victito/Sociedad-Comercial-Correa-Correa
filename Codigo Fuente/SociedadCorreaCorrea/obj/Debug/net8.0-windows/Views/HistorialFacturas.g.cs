﻿#pragma checksum "..\..\..\..\Views\HistorialFacturas.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AD5523FE717710AB91BD22370D1713BA6C243073"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using SociedadCorreaCorrea.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SociedadCorreaCorrea.Views {
    
    
    /// <summary>
    /// HistorialFacturas
    /// </summary>
    public partial class HistorialFacturas : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 124 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlControlBar;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximizar;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizar;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FiltroRazonSocial;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FiltroEstado;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\..\Views\HistorialFacturas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid facturasDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SociedadCorreaCorrea;component/views/historialfacturas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\HistorialFacturas.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 69 "..\..\..\..\Views\HistorialFacturas.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.IngresarFacturas_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 85 "..\..\..\..\Views\HistorialFacturas.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DatosEstadisticosFacturas_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pnlControlBar = ((System.Windows.Controls.StackPanel)(target));
            
            #line 130 "..\..\..\..\Views\HistorialFacturas.xaml"
            this.pnlControlBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Ventana_MouseBajo);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 137 "..\..\..\..\Views\HistorialFacturas.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnMaximizar = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\..\Views\HistorialFacturas.xaml"
            this.btnMaximizar.Click += new System.Windows.RoutedEventHandler(this.btnMaximizar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnMinimizar = ((System.Windows.Controls.Button)(target));
            
            #line 153 "..\..\..\..\Views\HistorialFacturas.xaml"
            this.btnMinimizar.Click += new System.Windows.RoutedEventHandler(this.btnMinimizar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FiltroRazonSocial = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.FiltroEstado = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.facturasDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 295 "..\..\..\..\Views\HistorialFacturas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EliminarFacturasSeleccionadas_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 300 "..\..\..\..\Views\HistorialFacturas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ActualizarFacturaSeleccionada_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 305 "..\..\..\..\Views\HistorialFacturas.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProductosFacturaSeleccionada_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

