using Microsoft.AspNetCore.Mvc;
using Sociedad_Correa_Web.App_Data;
using Sociedad_Correa_Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Sociedad_Correa_Web;

public class AccountController : Controller
{
    private readonly ContextoSMMS _context;

    public AccountController(ContextoSMMS context)
    {
        _context = context;
    }

    public IActionResult Logout()
    {
        // Eliminar todas las variables de sesión
        HttpContext.Session.Clear();

        // Redirigir al usuario a la página de inicio de sesión
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Login()
    {
        return View("~/Views/Home/Index.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string nombreUsuario, string clave)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
        {
            ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
            return View("~/Views/Home/Index.cshtml");
        }

        // Encriptar la contraseña ingresada
        var hashedPassword = HashPassword(clave);

        // Validar el usuario en la base de datos
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario &&
                                      u.Clave == hashedPassword &&
                                      u.IdEmpresa == GlobalSettings.IdEmpresa);

        if (usuario != null)
        {
            // Asignar variables de sesión
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            HttpContext.Session.SetString("Rol", usuario.Rol);
            HttpContext.Session.SetInt32("IdEmpresa", (int)usuario.IdEmpresa);
            HttpContext.Session.SetInt32("Id", (int)usuario.Id);

            // Consultar los datos del empleado relacionado con el Id del usuario
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(e => e.IdUsuario == usuario.Id);

            if (empleado != null)
            {
                // Guardar datos del empleado en la sesión
                HttpContext.Session.SetInt32("IdEmpleado", empleado.IdEmpleado);
                HttpContext.Session.SetString("NombreEmpleado", empleado.NombreEmpleado);
                HttpContext.Session.SetString("ApellidoEmpleado", empleado.ApellidoEmpleado);
                HttpContext.Session.SetString("PuestoEmpleado", empleado.PuestoEmpleado);
                HttpContext.Session.SetString("TelefonoEmpleado", empleado.TelefonoEmpleado);
            }

            // Redirigir según el rol del usuario
            if (usuario.Rol == "Administrativo")
            {
                return View("~/Views/Home/Privacy.cshtml");
            }
            else
            {
                return View("~/Views/Home/Trabajadores.cshtml");
            }
        }
        else
        {
            ViewBag.ErrorMessage = "Nombre de usuario o clave incorrectos.";
            return View("~/Views/Home/Index.cshtml");
        }
    }

    public async Task<IActionResult> ObtenerTareasDiarias()
    {
        // Obtener el IdEmpleado y IdEmpresa desde la sesión
        var idEmpleado = HttpContext.Session.GetInt32("IdEmpleado");
        var idEmpresa = HttpContext.Session.GetInt32("IdEmpresa");

        // Validar si los valores de sesión existen
        if (idEmpleado == null || idEmpresa == null)
        {
            ViewBag.ErrorMessage = "No se encontraron datos de sesión. Inicie sesión nuevamente.";
            return RedirectToAction("Login");
        }

        // Consultar las tareas relacionadas con el empleado y la empresa
        var tareas = await _context.TareasDiarias
            .Where(t => t.IdEmpleado == idEmpleado && t.IdEmpresa == idEmpresa)
            .ToListAsync();

        // Pasar las tareas a la vista
        return View("~/Views/Home/TareasDiarias.cshtml", tareas);
    }

    public IActionResult Back()
    {
        return View("~/Views/Home/Trabajadores.cshtml");
    }

    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
