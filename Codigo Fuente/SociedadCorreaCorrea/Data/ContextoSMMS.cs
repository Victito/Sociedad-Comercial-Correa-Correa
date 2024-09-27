using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SociedadCorreaCorrea.Models;
using System.Configuration;

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

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<HorarioEmpleado> HorarioEmpleados { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["SociedadCorreacorreaDB"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297C66A41F89");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estatus");
            entity.Property(e => e.FechaContratacion).HasColumnName("fechaContratacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
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
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__3CD5687EFCB28F66");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Facturas_Empleado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Facturas_Empresa");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Facturas_Sucursal");
        });

        modelBuilder.Entity<HorarioEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__HorarioE__DE60F33AF1DC19C7");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.DiaSemana)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("diaSemana");
            entity.Property(e => e.HoraEntrada)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("horaEntrada");
            entity.Property(e => e.HoraSalida)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("horaSalida");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.HorarioEmpleados)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_HorarioEmpleados_Empleado");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.HorarioEmpleados)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HorarioEmpleados_Empresa");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.HorarioEmpleados)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_HorarioEmpleados_Sucursal");
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK__Sesiones__DB6C2DE66A26F05D");

            entity.Property(e => e.IdSesion).HasColumnName("idSesion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IpOrigen)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ipOrigen");
            entity.Property(e => e.TokenSesion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tokenSesion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Sesiones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Sesiones_Usuario");
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
