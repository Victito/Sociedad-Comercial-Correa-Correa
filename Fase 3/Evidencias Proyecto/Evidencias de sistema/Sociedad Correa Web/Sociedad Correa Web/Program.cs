using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.App_Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.

// Habilitar controladores con vistas.
builder.Services.AddControllersWithViews();

// Registrar el DbContext con la conexi�n a SQL Server.
builder.Services.AddDbContext<ContextoSMMS>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar el servicio de sesi�n.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n.
    options.Cookie.HttpOnly = true; // Seguridad adicional para cookies.
    options.Cookie.IsEssential = true; // Asegura que las cookies de sesi�n sean esenciales.
});

// Configurar la autenticaci�n basada en cookies.
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Account/Login"; // Ruta para iniciar sesi�n.
        options.LogoutPath = "/Account/Logout"; // Ruta para cerrar sesi�n.
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta si no tiene permiso.
    });

// Construir la aplicaci�n.
var app = builder.Build();

// Configurar el middleware para manejar solicitudes.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Configuraci�n de HSTS para producci�n.
}

// Middleware general.
app.UseHttpsRedirection(); // Redirigir autom�ticamente a HTTPS.
app.UseStaticFiles(); // Servir archivos est�ticos (CSS, JS, im�genes, etc.).

app.UseRouting(); // Configurar enrutamiento.

// Habilitar el uso de autenticaci�n y autorizaci�n.
app.UseAuthentication();
app.UseAuthorization();

// Habilitar el uso de sesiones.
app.UseSession();

// Configuraci�n de las rutas predeterminadas.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicaci�n.
app.Run();
