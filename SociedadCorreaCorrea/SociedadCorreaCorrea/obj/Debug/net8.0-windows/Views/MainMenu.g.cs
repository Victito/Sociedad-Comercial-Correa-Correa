﻿#pragma checksum "..\..\..\..\Views\MainMenu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43645F58870ED92BB4431C553A022FAA65C6D971"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
    /// MainMenu
    /// </summary>
    public partial class MainMenu : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 137 "..\..\..\..\Views\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlControlBar;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\..\Views\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\Views\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximizar;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\..\Views\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SociedadCorreaCorrea;component/views/mainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 73 "..\..\..\..\Views\MainMenu.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.IngresarFacturas_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 81 "..\..\..\..\Views\MainMenu.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.HistorialFacturas_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 90 "..\..\..\..\Views\MainMenu.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DatosEstadisticosFacturas_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 99 "..\..\..\..\Views\MainMenu.xaml"
            ((System.Windows.Controls.RadioButton)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.AdministradorDeTrabajadores_ClickMouse);
            
            #line default
            #line hidden
            return;
            case 5:
            this.pnlControlBar = ((System.Windows.Controls.StackPanel)(target));
            
            #line 143 "..\..\..\..\Views\MainMenu.xaml"
            this.pnlControlBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Ventana_MouseBajo);
            
            #line default
            #line hidden
            
            #line 144 "..\..\..\..\Views\MainMenu.xaml"
            this.pnlControlBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.panelControl_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 149 "..\..\..\..\Views\MainMenu.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnMaximizar = ((System.Windows.Controls.Button)(target));
            
            #line 157 "..\..\..\..\Views\MainMenu.xaml"
            this.btnMaximizar.Click += new System.Windows.RoutedEventHandler(this.btnMaximizar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnMinimizar = ((System.Windows.Controls.Button)(target));
            
            #line 165 "..\..\..\..\Views\MainMenu.xaml"
            this.btnMinimizar.Click += new System.Windows.RoutedEventHandler(this.btnMinimizar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

