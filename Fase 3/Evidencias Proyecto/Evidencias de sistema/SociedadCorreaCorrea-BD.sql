USE [sociedadcorreacorrea_bd]
GO
/****** Object:  Table [dbo].[Acuses]    Script Date: 15-12-2024 21:52:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Acuses](
	[idAcuse] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[recinto] [varchar](100) NOT NULL,
	[fecha] [date] NOT NULL,
	[rut] [varchar](20) NOT NULL,
	[firma] [varchar](255) NULL,
	[idFactura] [int] NOT NULL,
	[idSucursal] [int] NULL,
	[idEmpresa] [int] NOT NULL,
	[numero_factura] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idAcuse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuracion]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Clave] [nvarchar](255) NOT NULL,
	[Valor] [nvarchar](255) NOT NULL,
	[IdEmpresa] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[idEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[idSucursal] [int] NULL,
	[idUsuario] [bigint] NULL,
	[nombreEmpleado] [varchar](100) NOT NULL,
	[apellidoEmpleado] [varchar](100) NOT NULL,
	[fechaNacimientoEmpleado] [date] NULL,
	[direccionEmpleado] [varchar](255) NULL,
	[telefonoEmpleado] [varchar](20) NULL,
	[correoEmpleado] [varchar](100) NULL,
	[puestoEmpleado] [varchar](100) NULL,
	[salarioEmpleado] [decimal](10, 2) NULL,
	[fechaContratacionEmpleado] [date] NULL,
	[estatusEmpleado] [varchar](20) NULL,
	[idTurno] [int] NULL,
	[RutEmpleado] [varchar](50) NULL,
	[idTurnoPersonalizado] [int] NULL,
	[tareasEmpleado] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[idEmpresa] [int] IDENTITY(1,1) NOT NULL,
	[nombreEmpresa] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[entrada_salida]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[entrada_salida](
	[idRegistro] [int] IDENTITY(1,1) NOT NULL,
	[idEmpleado] [int] NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[hora_entrada] [time](7) NULL,
	[hora_salida] [time](7) NULL,
	[latitud_entrada] [decimal](9, 6) NULL,
	[longitud_entrada] [decimal](9, 6) NULL,
	[latitud_salida] [decimal](9, 6) NULL,
	[longitud_salida] [decimal](9, 6) NULL,
PRIMARY KEY CLUSTERED 
(
	[idRegistro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [bigint] NOT NULL,
	[idSucursal] [int] NULL,
	[idEmpresa] [int] NOT NULL,
	[razon_social] [varchar](250) NULL,
	[rut_emisor] [varchar](250) NULL,
	[giro] [varchar](250) NULL,
	[direccion] [varchar](250) NULL,
	[comuna] [varchar](250) NULL,
	[ciudad] [varchar](250) NULL,
	[entregar_en] [varchar](250) NULL,
	[fecha_emision] [date] NULL,
	[fecha_vencimiento] [date] NULL,
	[cobrador] [varchar](250) NULL,
	[nota_venta] [int] NULL,
	[orden_compra] [varchar](250) NULL,
	[condiciones] [varchar](250) NULL,
	[guia_despacho] [varchar](250) NULL,
	[precio_unitario] [decimal](10, 2) NULL,
	[cantidad] [int] NULL,
	[total] [decimal](10, 2) NULL,
	[estado] [varchar](250) NULL,
	[rut_vendedor] [varchar](250) NULL,
	[giro_vendedor] [varchar](250) NULL,
	[razon_social_vendedor] [varchar](250) NULL,
	[numero_factura] [int] NULL,
	[ColorVencimiento] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[codigo_producto] [varchar](50) NOT NULL,
	[descripcion] [varchar](255) NOT NULL,
	[n_serie] [varchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unitario] [decimal](10, 2) NOT NULL,
	[descuento] [int] NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[idFactura] [int] NOT NULL,
	[idSucursal] [int] NULL,
	[idEmpresa] [int] NOT NULL,
	[numero_factura] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puestos]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puestos](
	[idPuestos] [int] IDENTITY(1,1) NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[nombre_puesto] [nvarchar](100) NOT NULL,
	[estado_puesto] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPuestos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[ServicioId] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaId] [int] NOT NULL,
	[SucursalId] [int] NOT NULL,
	[NombreServicio] [nvarchar](250) NOT NULL,
	[CostoServicio] [decimal](18, 2) NOT NULL,
	[EmpresaServicio] [nvarchar](250) NOT NULL,
	[FechaContratacion] [date] NULL,
	[FechaPago] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ServicioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[idSucursal] [int] IDENTITY(1,1) NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[nombreSucursal] [varchar](100) NOT NULL,
	[direccion] [varchar](255) NULL,
	[ciudad] [varchar](100) NULL,
	[pais] [varchar](100) NULL,
	[codigoPostal] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TareasDiarias]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TareasDiarias](
	[TareaDiariaId] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaId] [int] NOT NULL,
	[EmpleadoId] [int] NOT NULL,
	[NombreTarea] [nvarchar](250) NOT NULL,
	[DescripcionTarea] [nvarchar](500) NULL,
	[FechaTarea] [date] NOT NULL,
	[HoraInicioTarea] [time](7) NOT NULL,
	[HoraTerminoTarea] [time](7) NOT NULL,
	[SucursalId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TareaDiariaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turno]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turno](
	[idTurno] [int] IDENTITY(1,1) NOT NULL,
	[idSucursal] [int] NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[nombreTurno] [nvarchar](50) NOT NULL,
	[diaSemanaTurno] [varchar](20) NOT NULL,
	[horaInicioTurno] [time](7) NOT NULL,
	[horaFinTurno] [time](7) NOT NULL,
	[horaAlmuerzoInicioTurno] [time](7) NOT NULL,
	[horaAlmuerzoFinTurno] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTurno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 15-12-2024 21:52:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[nombre_usuario] [varchar](255) NOT NULL,
	[clave] [varchar](255) NOT NULL,
	[rol] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Empleados] ADD  DEFAULT ('Activo') FOR [estatusEmpleado]
GO
ALTER TABLE [dbo].[Facturas] ADD  DEFAULT ('Pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[Acuses]  WITH CHECK ADD  CONSTRAINT [FK_Acuses_Factura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[Facturas] ([idFactura])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Acuses] CHECK CONSTRAINT [FK_Acuses_Factura]
GO
ALTER TABLE [dbo].[Configuracion]  WITH CHECK ADD  CONSTRAINT [FK_Configuracion_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([idEmpresa])
GO
ALTER TABLE [dbo].[Configuracion] CHECK CONSTRAINT [FK_Configuracion_Empresa]
GO
ALTER TABLE [dbo].[Empleados]  WITH NOCHECK ADD  CONSTRAINT [FK_Empleados_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([idEmpresa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleados] NOCHECK CONSTRAINT [FK_Empleados_Empresa]
GO
ALTER TABLE [dbo].[Empleados]  WITH NOCHECK ADD  CONSTRAINT [FK_Empleados_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[Empleados] NOCHECK CONSTRAINT [FK_Empleados_Sucursal]
GO
ALTER TABLE [dbo].[Empleados]  WITH NOCHECK ADD  CONSTRAINT [FK_Empleados_Turno] FOREIGN KEY([idTurno])
REFERENCES [dbo].[Turno] ([idTurno])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleados] NOCHECK CONSTRAINT [FK_Empleados_Turno]
GO
ALTER TABLE [dbo].[Empleados]  WITH NOCHECK ADD  CONSTRAINT [FK_Empleados_Usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([id])
GO
ALTER TABLE [dbo].[Empleados] NOCHECK CONSTRAINT [FK_Empleados_Usuarios]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Usuario]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Factura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[Facturas] ([idFactura])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Factura]
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresa] ([idEmpresa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([idEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Empresa]
GO
ALTER TABLE [dbo].[TareasDiarias]  WITH CHECK ADD  CONSTRAINT [FK_TareasDiarias_EmpleadoId] FOREIGN KEY([EmpleadoId])
REFERENCES [dbo].[Empleados] ([idEmpleado])
GO
ALTER TABLE [dbo].[TareasDiarias] CHECK CONSTRAINT [FK_TareasDiarias_EmpleadoId]
GO
ALTER TABLE [dbo].[TareasDiarias]  WITH CHECK ADD  CONSTRAINT [FK_TareasDiarias_Empresa] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresa] ([idEmpresa])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TareasDiarias] CHECK CONSTRAINT [FK_TareasDiarias_Empresa]
GO
ALTER TABLE [dbo].[TareasDiarias]  WITH CHECK ADD  CONSTRAINT [FK_TareasDiarias_SucursalId] FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursal] ([idSucursal])
GO
ALTER TABLE [dbo].[TareasDiarias] CHECK CONSTRAINT [FK_TareasDiarias_SucursalId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([idEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Empresa]
GO
ALTER TABLE [dbo].[Puestos]  WITH CHECK ADD CHECK  (([estado_puesto]='Inactivo' OR [estado_puesto]='Activo'))
GO
ALTER TABLE [dbo].[Puestos]  WITH CHECK ADD CHECK  (([estado_puesto]='Inactivo' OR [estado_puesto]='Activo'))
GO
