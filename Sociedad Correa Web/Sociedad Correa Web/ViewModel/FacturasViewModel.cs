namespace Sociedad_Correa_Web.ViewModels
{
    public class FacturaViewModel
    {
        public string? RutEmisor { get; set; }
        public string? Comuna { get; set; }
        public string? Ciudad { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
        public string? Estado { get; set; }
        public string? RutVendedor { get; set; }
        public string? GiroVendedor { get; set; }
        public int? DiasRestantes { get; set; }
    }
}
