using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sociedad_Correa_Web.Models;

namespace Sociedad_Correa_Web.App_Data;

public partial class ContextoSMMS : DbContext
{
    public ContextoSMMS()
    {
    }

    public ContextoSMMS(DbContextOptions<ContextoSMMS> options)
        : base(options)
    {
    }

    public virtual DbSet<Acuse> Acuses { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EntradaSalidum> EntradaSalida { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TareasDiaria> TareasDiarias { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<TurnoPersonalizado> TurnoPersonalizados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MAXIPC;Database=sociedadcorreacorrea_bd;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acuse>(entity =>
        {
            entity.HasKey(e => e.IdAcuse).HasName("PK__Acuses__288B2F1CC32F743E");

            entity.Property(e => e.IdAcuse).HasColumnName("idAcuse");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Firma)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("firma");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Recinto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("recinto");
            entity.Property(e => e.Rut)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rut");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Acuses)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Acuses_Factura");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Configur__3214EC07C02361CD");

            entity.ToTable("Configuracion");

            entity.Property(e => e.Clave).HasMaxLength(255);
            entity.Property(e => e.Valor).HasMaxLength(255);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Configuracions)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Configuracion_Empresa");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297C9F91B538");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.ApellidoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidoEmpleado");
            entity.Property(e => e.CorreoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correoEmpleado");
            entity.Property(e => e.DireccionEmpleado)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccionEmpleado");
            entity.Property(e => e.EstatusEmpleado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estatusEmpleado");
            entity.Property(e => e.FechaContratacionEmpleado).HasColumnName("fechaContratacionEmpleado");
            entity.Property(e => e.FechaNacimientoEmpleado).HasColumnName("fechaNacimientoEmpleado");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.IdTurnoPersonalizado).HasColumnName("idTurnoPersonalizado");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreEmpleado");
            entity.Property(e => e.PuestoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("puestoEmpleado");
            entity.Property(e => e.RutEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SalarioEmpleado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salarioEmpleado");
            entity.Property(e => e.TareasEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tareasEmpleado");
            entity.Property(e => e.TelefonoEmpleado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefonoEmpleado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Empleados_Empresa");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Empleados_Sucursal");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Empleados_Turno");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Empleados_Usuarios");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__75D2CED4648FD7A2");

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreEmpresa");
        });

        modelBuilder.Entity<EntradaSalidum>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__entrada___62FC8F587773EC45");

            entity.ToTable("entrada_salida");

            entity.Property(e => e.IdRegistro).HasColumnName("idRegistro");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada).HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.LatitudEntrada)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitud_entrada");
            entity.Property(e => e.LatitudSalida)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitud_salida");
            entity.Property(e => e.LongitudEntrada)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitud_entrada");
            entity.Property(e => e.LongitudSalida)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitud_salida");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__3CD5687E8A161E1F");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Cobrador)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("cobrador");
            entity.Property(e => e.Comuna)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("comuna");
            entity.Property(e => e.Condiciones)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("condiciones");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.EntregarEn)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("entregar_en");
            entity.Property(e => e.Estado)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.Giro)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("giro");
            entity.Property(e => e.GiroVendedor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("giro_vendedor");
            entity.Property(e => e.GuiaDespacho)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("guia_despacho");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NotaVenta).HasColumnName("nota_venta");
            entity.Property(e => e.OrdenCompra)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("orden_compra");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("razon_social");
            entity.Property(e => e.RazonSocialVendedor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("razon_social_vendedor");
            entity.Property(e => e.RutEmisor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("rut_emisor");
            entity.Property(e => e.RutVendedor)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("rut_vendedor");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Facturas_Usuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132843BA1CC");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Descuento).HasColumnName("descuento");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.NSerie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("n_serie");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Productos_Factura");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuestos).HasName("PK__Puestos__2373482082B0550D");

            entity.Property(e => e.IdPuestos).HasColumnName("idPuestos");
            entity.Property(e => e.EstadoPuesto)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado_puesto");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombrePuesto)
                .HasMaxLength(100)
                .HasColumnName("nombre_puesto");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("PK__Servicio__D5AEECC20B351525");

            entity.Property(e => e.CostoServicio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmpresaServicio).HasMaxLength(250);
            entity.Property(e => e.NombreServicio).HasMaxLength(250);

            entity.HasOne(d => d.Empresa).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK_Servicios_Empresa");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694C1F6948E3");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigoPostal");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreSucursal");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pais");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Sucursal_Empresa");
        });

        modelBuilder.Entity<TareasDiaria>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DescripcionTarea).HasMaxLength(500);
            entity.Property(e => e.NombreTarea).HasMaxLength(250);
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__Turno__AA068B01543ADE74");

            entity.ToTable("Turno");

            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.DiaSemanaTurno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("diaSemanaTurno");
            entity.Property(e => e.HoraAlmuerzoFinTurno).HasColumnName("horaAlmuerzoFinTurno");
            entity.Property(e => e.HoraAlmuerzoInicioTurno).HasColumnName("horaAlmuerzoInicioTurno");
            entity.Property(e => e.HoraFinTurno).HasColumnName("horaFinTurno");
            entity.Property(e => e.HoraInicioTurno).HasColumnName("horaInicioTurno");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.NombreTurno)
                .HasMaxLength(50)
                .HasColumnName("nombreTurno");
        });

        modelBuilder.Entity<TurnoPersonalizado>(entity =>
        {
            entity.HasKey(e => e.IdTurnoPersonalizado).HasName("PK__Turno_Pe__8A756A7A835812AA");

            entity.ToTable("Turno_Personalizado");

            entity.Property(e => e.IdTurnoPersonalizado).HasColumnName("idTurnoPersonalizado");
            entity.Property(e => e.DiaSemanaPersonalizado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("diaSemanaPersonalizado");
            entity.Property(e => e.FechaTurno).HasColumnName("fechaTurno");
            entity.Property(e => e.HoraAlmuerzoFinTp).HasColumnName("horaAlmuerzoFinTP");
            entity.Property(e => e.HoraAlmuerzoInicioTp).HasColumnName("horaAlmuerzoInicioTP");
            entity.Property(e => e.HoraFinTp).HasColumnName("horaFinTP");
            entity.Property(e => e.HoraInicioTp).HasColumnName("horaInicioTP");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.ObservacionTp)
                .HasMaxLength(200)
                .HasColumnName("observacionTP");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TurnoPersonalizados)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurnoPersonalizado_Empleado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F271497E5");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__D4D22D74327C83FC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Usuarios_Empresa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
