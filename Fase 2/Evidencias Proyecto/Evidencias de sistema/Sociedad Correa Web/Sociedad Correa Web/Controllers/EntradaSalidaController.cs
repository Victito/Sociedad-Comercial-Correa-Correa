using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.App_Data;
using Sociedad_Correa_Web.Models;
using System;
using System.Threading.Tasks;

public class EntradaSalidaController : Controller
{
    private readonly ContextoSMMS _context;

    public EntradaSalidaController(ContextoSMMS context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarEntrada(double latitud, double longitud)
    {
        try
        {
            var idEmpleado = HttpContext.Session.GetInt32("IdEmpleado");
            var idEmpresa = HttpContext.Session.GetInt32("IdEmpresa");

            if (!idEmpleado.HasValue || !idEmpresa.HasValue)
            {
                ViewBag.Message = "No se pudo obtener el empleado o la empresa de la sesión.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            var zonaHorariaChile = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            var fechaHoraActualChile = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHorariaChile);

            if (fechaHoraActualChile.TimeOfDay < new TimeSpan(7, 0, 0))
            {
                ViewBag.Message = "No puedes registrar la hora de entrada antes de las 7:00 AM.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            var fechaHoy = DateOnly.FromDateTime(fechaHoraActualChile);

            var entradaExistente = await _context.EntradaSalida
                .FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado.Value &&
                                          e.IdEmpresa == idEmpresa.Value &&
                                          e.Fecha == fechaHoy);

            if (entradaExistente != null)
            {
                ViewBag.Message = "Ya se ha registrado una entrada para hoy.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            var nuevaEntrada = new EntradaSalidum
            {
                IdEmpleado = idEmpleado.Value,
                IdEmpresa = idEmpresa.Value,
                Fecha = fechaHoy,
                HoraEntrada = TimeOnly.FromDateTime(fechaHoraActualChile),
                LatitudEntrada = (decimal?)latitud,
                LongitudEntrada = (decimal?)longitud
            };

            _context.EntradaSalida.Add(nuevaEntrada);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Hora de entrada registrada correctamente.";
            ViewBag.Success = true;
            return View("~/Views/Home/Trabajadores.cshtml");
        }
        catch (Exception ex)
        {
            ViewBag.Message = "Error al registrar la hora de entrada.";
            ViewBag.Success = false;
            ViewBag.Error = ex.Message;
            return View("~/Views/Home/Trabajadores.cshtml");
        }
    }

    [HttpPost]
    public async Task<IActionResult> MarcarSalida(double latitud, double longitud)
    {
        try
        {
            var idEmpleado = HttpContext.Session.GetInt32("IdEmpleado");
            var idEmpresa = HttpContext.Session.GetInt32("IdEmpresa");

            if (!idEmpleado.HasValue || !idEmpresa.HasValue)
            {
                ViewBag.Message = "No se pudo obtener el empleado o la empresa de la sesión.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            var zonaHorariaChile = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            var fechaHoraActualChile = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHorariaChile);

            if (fechaHoraActualChile.TimeOfDay > new TimeSpan(21, 0, 0))
            {
                ViewBag.Message = "No puedes marcar la hora de salida después de las 9:00 PM.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            var fechaHoy = DateOnly.FromDateTime(fechaHoraActualChile);

            var registroEntrada = await _context.EntradaSalida
                .FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado.Value &&
                                          e.IdEmpresa == idEmpresa.Value &&
                                          e.Fecha == fechaHoy);

            if (registroEntrada == null)
            {
                ViewBag.Message = "No se encontró un registro de entrada para hoy.";
                ViewBag.Success = false;
                return View("~/Views/Home/Trabajadores.cshtml");
            }

            registroEntrada.HoraSalida = TimeOnly.FromDateTime(fechaHoraActualChile);
            registroEntrada.LatitudSalida = (decimal?)latitud;
            registroEntrada.LongitudSalida = (decimal?)longitud;

            _context.EntradaSalida.Update(registroEntrada);
            await _context.SaveChangesAsync();
            ViewBag.Message = "Hora de salida registrada correctamente.";
            ViewBag.Success = true;
            return View("~/Views/Home/Trabajadores.cshtml");
        }
        catch (Exception ex)
        {
            ViewBag.Message = "Error al marcar la hora de salida.";
            ViewBag.Success = false;
            ViewBag.Error = ex.Message;
            return View("~/Views/Home/Trabajadores.cshtml");
        }
    }
}
