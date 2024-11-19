using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.App_Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.

// Habilitar controladores con vistas.
builder.Services.AddControllersWithViews();

// Registrar el DbContext con la conexión a SQL Server.
builder.Services.AddDbContext<ContextoSMMS>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar el servicio de sesión.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión.
    options.Cookie.HttpOnly = true; // Seguridad adicional para cookies.
    options.Cookie.IsEssential = true; // Asegura que las cookies de sesión sean esenciales.
});

// Configurar la autenticación basada en cookies.
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Account/Login"; // Ruta para iniciar sesión.
        options.LogoutPath = "/Account/Logout"; // Ruta para cerrar sesión.
        options.AccessDeniedPath = "/Home/AccessDenied"; // Ruta si no tiene permiso.
    });

// Construir la aplicación.
var app = builder.Build();

// Configurar el middleware para manejar solicitudes.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Configuración de HSTS para producción.
}

// Middleware general.
app.UseHttpsRedirection(); // Redirigir automáticamente a HTTPS.
app.UseStaticFiles(); // Servir archivos estáticos (CSS, JS, imágenes, etc.).

app.UseRouting(); // Configurar enrutamiento.

// Habilitar el uso de autenticación y autorización.
app.UseAuthentication();
app.UseAuthorization();

// Habilitar el uso de sesiones.
app.UseSession();

// Configuración de las rutas predeterminadas.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación.
app.Run();
