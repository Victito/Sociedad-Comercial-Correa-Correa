using Microsoft.AspNetCore.Mvc;
using Sociedad_Correa_Web;
using Sociedad_Correa_Web.ViewModel;

namespace Sociedad_Correa_Web
{
    public static class GlobalSettings
    {
        // Cambia el tipo a long si `IdEmpresa` en la base de datos es `long`
        public static long IdEmpresa { get; set; } = 1;
    }
}

