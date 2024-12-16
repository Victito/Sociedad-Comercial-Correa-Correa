using Microsoft.AspNetCore.Mvc;
using Sociedad_Correa_Web.App_Data;
using Sociedad_Correa_Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sociedad_Correa_Web;

public class AccountController : Controller
{
    private readonly ContextoSMMS _context;

    public AccountController(ContextoSMMS context)
    {
        _context = context;
    }

    /// <summary>
    /// Acción para cerrar sesión.
    /// </summary>
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    /// <summary>
    /// Muestra la vista de inicio de sesión.
    /// </summary>
    public IActionResult Login()
    {
        return View("~/Views/Home/Index.cshtml");
    }

    /// <summary>
    /// Procesa la solicitud de inicio de sesión.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login(string nombreUsuario, string clave)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
        {
            ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
            return View("~/Views/Home/Index.cshtml");
        }

        try
        {
            var hashedPassword = HashPassword(clave);

            // Validar el usuario en la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario &&
                                          u.Clave == hashedPassword &&
                                          u.IdEmpresa == GlobalSettings.IdEmpresa);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = "Nombre de usuario o clave incorrectos.";
                return View("~/Views/Home/Index.cshtml");
            }

            // Asignar variables de sesión
            SetUserSession(usuario);

            // Consultar los datos del empleado relacionado
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(e => e.IdUsuario == usuario.Id);

            if (empleado != null)
            {
                SetEmployeeSession(empleado);
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
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
            return View("~/Views/Home/Index.cshtml");
        }
    }

    /// <summary>
    /// Obtiene las tareas diarias para el empleado autenticado.
    /// </summary>
    public async Task<IActionResult> ObtenerTareasDiarias()
    {
        try
        {
            var idEmpleado = HttpContext.Session.GetInt32("IdEmpleado");
            var idEmpresa = HttpContext.Session.GetInt32("IdEmpresa");

            if (idEmpleado == null || idEmpresa == null)
            {
                return RedirectToAction("Login", new { errorMessage = "Por favor, inicie sesión nuevamente." });
            }

            var tareas = await _context.TareasDiarias
                .Where(t => t.EmpleadoId == idEmpleado && t.EmpresaId == idEmpresa)
                .ToListAsync();

            return View("~/Views/Home/TareasDiarias.cshtml", tareas);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error al obtener las tareas: {ex.Message}";
            return RedirectToAction("Login");
        }
    }

    /// <summary>
    /// Redirige a la página principal de los trabajadores.
    /// </summary>
    public IActionResult Back()
    {
        return View("~/Views/Home/Trabajadores.cshtml");
    }

    /// <summary>
    /// Método auxiliar para encriptar contraseñas.
    /// </summary>
    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    /// <summary>
    /// Establece la sesión del usuario.
    /// </summary>
    private void SetUserSession(Usuario usuario)
    {
        HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
        HttpContext.Session.SetString("Rol", usuario.Rol);
        HttpContext.Session.SetInt32("IdEmpresa", usuario.IdEmpresa);
        HttpContext.Session.SetInt32("Id", (int)usuario.Id);
    }

    /// <summary>
    /// Establece la sesión del empleado.
    /// </summary>
    private void SetEmployeeSession(Empleado empleado)
    {
        HttpContext.Session.SetInt32("IdEmpleado", empleado.IdEmpleado);
        HttpContext.Session.SetString("NombreEmpleado", empleado.NombreEmpleado);
        HttpContext.Session.SetString("ApellidoEmpleado", empleado.ApellidoEmpleado);
        HttpContext.Session.SetString("PuestoEmpleado", empleado.PuestoEmpleado);
        HttpContext.Session.SetString("TelefonoEmpleado", empleado.TelefonoEmpleado);
        HttpContext.Session.SetString("TareasEmpleado", empleado.TareasEmpleado);
    }
}
