﻿#pragma checksum "..\..\..\..\Views\LoginVista.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A32DD5A367371CEE5709CA4EE475335CA0B5DB27"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
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
using prueba.Vista;


namespace prueba.Vista {
    
    
    /// <summary>
    /// LoginVista
    /// </summary>
    public partial class LoginVista : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 74 "..\..\..\..\Views\LoginVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizar;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Views\LoginVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\..\Views\LoginVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtusuario;
        
        #line default
        #line hidden
        
        
        #line 200 "..\..\..\..\Views\LoginVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtContraseña;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\..\Views\LoginVista.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIngresar;
        
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
            System.Uri resourceLocater = new System.Uri("/SociedadCorreaCorrea;component/views/loginvista.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\LoginVista.xaml"
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
            
            #line 59 "..\..\..\..\Views\LoginVista.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Ventana_MouseBajo);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimizar = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\..\Views\LoginVista.xaml"
            this.btnMinimizar.Click += new System.Windows.RoutedEventHandler(this.btnMinimizar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\..\Views\LoginVista.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtusuario = ((System.Windows.Controls.TextBox)(target));
            
            #line 184 "..\..\..\..\Views\LoginVista.xaml"
            this.txtusuario.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtusuario_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtContraseña = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 212 "..\..\..\..\Views\LoginVista.xaml"
            this.txtContraseña.PasswordChanged += new System.Windows.RoutedEventHandler(this.txtContraseña_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnIngresar = ((System.Windows.Controls.Button)(target));
            
            #line 226 "..\..\..\..\Views\LoginVista.xaml"
            this.btnIngresar.Click += new System.Windows.RoutedEventHandler(this.NavigateToFacturasPage);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

