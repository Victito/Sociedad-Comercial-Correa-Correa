using Microsoft.AspNetCore.Mvc;

namespace Sociedad_Correa_Web.Controllers
{
    public class RecuperarDatosSession : Controller
    {
        public IActionResult session()
        {
            var nombreUsuario = HttpContext.Session.GetString("NombreUsuario");
            var rolUsuario = HttpContext.Session.GetString("Rol");

            ViewBag.NombreUsuario = nombreUsuario;
            ViewBag.RolUsuario = rolUsuario;

            return View();
        }
    }
}
