using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.App_Data;
using System.Threading.Tasks;

public class EmpleadoController : Controller
{
    private readonly ContextoSMMS _context;

    public EmpleadoController(ContextoSMMS context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerDatosEmpleado()
    {
        // Obtener el ID del usuario desde la sesión
        var idUsuario = HttpContext.Session.GetInt32("Id");

        if (idUsuario == null)
        {
            // Si el usuario no ha iniciado sesión, redirige al login
            return RedirectToAction("Login", "Account");
        }

        // Buscar los datos del empleado relacionado al usuario
        var empleado = await _context.Empleados
            .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);

        if (empleado == null)
        {
            // Manejar el caso en el que no se encuentra al empleado
            ViewBag.ErrorMessage = "No se encontraron datos del empleado.";
            return View("~/Views/Home/Error.cshtml");
        }

        // Retornar una vista con los datos del empleado
        return View("~/Views/Home/=Trabajadores.cshtml", empleado);
    }
}
