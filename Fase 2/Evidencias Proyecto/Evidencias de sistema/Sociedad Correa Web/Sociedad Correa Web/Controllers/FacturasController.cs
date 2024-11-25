using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.App_Data;
using Sociedad_Correa_Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

public class FacturasController : Controller
{
    private readonly ContextoSMMS _context;

    public FacturasController(ContextoSMMS context)
    {
        _context = context;
    }

    public async Task<IActionResult> MostrarFacturas()
    {
        // Obtener las facturas desde la base de datos
        var facturas = await _context.Facturas
            .Select(f => new
            {
                f.RutEmisor,
                f.Comuna,
                f.Ciudad,
                f.FechaEmision,
                f.FechaVencimiento,
                f.Cantidad,
                f.Total,
                f.Estado,
                f.RutVendedor,
                f.GiroVendedor
            })
            .ToListAsync();

        // Calcular los días restantes después de obtener los datos
        var facturasViewModel = facturas.Select(f => new FacturaViewModel
        {
            RutEmisor = f.RutEmisor,
            Comuna = f.Comuna,
            Ciudad = f.Ciudad,
            FechaEmision = f.FechaEmision.HasValue
                ? f.FechaEmision.Value.ToDateTime(TimeOnly.MinValue)
                : (DateTime?)null,
            FechaVencimiento = f.FechaVencimiento.HasValue
                ? f.FechaVencimiento.Value.ToDateTime(TimeOnly.MinValue)
                : (DateTime?)null,
            Cantidad = f.Cantidad,
            Total = f.Total,
            Estado = f.Estado,
            RutVendedor = f.RutVendedor,
            GiroVendedor = f.GiroVendedor,
            DiasRestantes = f.FechaVencimiento.HasValue
                ? (f.FechaVencimiento.Value.ToDateTime(TimeOnly.MinValue) - DateTime.Now).Days
                : (int?)null // Si la fecha de vencimiento es nula
        }).ToList();

        return View("~/Views/Home/Facturas.cshtml", facturasViewModel);
    }
    public IActionResult Back()
    {
        return View("~/Views/Home/Privacy.cshtml");
    }
}
