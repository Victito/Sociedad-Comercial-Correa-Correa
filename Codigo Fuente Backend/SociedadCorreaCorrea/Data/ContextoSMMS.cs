using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SociedadCorreaCorrea.Models;

namespace SociedadCorreaCorrea.Data;

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

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["SociedadCorreacorreaDB"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acuse>(entity =>
        {
            entity.HasKey(e => e.IdAcuse).HasName("PK__Acuses__288B2F1CEA09040E");

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

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297C46CCB77A");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estatus");
            entity.Property(e => e.FechaContratacion).HasColumnName("fechaContratacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("puesto");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Empleados_Empresa");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Empleados_Sucursal");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Empleados_Usuarios");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__75D2CED4A3686E61");

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreEmpresa");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__3CD5687E30F8ABD6");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Cobrador)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cobrador");
            entity.Property(e => e.Comuna)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("comuna");
            entity.Property(e => e.Condiciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("condiciones");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.EntregarEn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("entregar_en");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.Giro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("giro");
            entity.Property(e => e.GuiaDespacho)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("guia_despacho");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NotaVenta).HasColumnName("nota_venta");
            entity.Property(e => e.OrdenCompra)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("orden_compra");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razon_social");
            entity.Property(e => e.RutEmisor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rut_emisor");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Facturas_Usuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A13202BB324D");

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

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694C41D754D0");

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

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83F8DB23C31");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__D4D22D7485CD3324").IsUnique();

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
