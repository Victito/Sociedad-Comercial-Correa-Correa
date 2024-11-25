using SociedadCorreaCorrea.ViewModels;
using SociedadCorreaCorrea.ViewsModels;
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

namespace SociedadCorreaCorrea.Views
{
    /// <summary>
    /// Interaction logic for PantallaCarga.xaml
    /// </summary>
    public partial class PantallaCarga : Window
    {
        public PantallaCarga()
        {
            InitializeComponent();
            this.DataContext = new PantallaCargaViewModel(); // Establecer el DataContext
        }
    }
}
