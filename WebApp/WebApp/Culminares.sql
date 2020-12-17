Create database CULMINARES
GO 
USE CULMINARES
GO

CREATE TABLE [dbo].[Estado](
	[Estado_Id] [char](1) NOT NULL,
	[Estado_Nombre] [nchar](10) NOT NULL,
 CONSTRAINT [PK_estado] PRIMARY KEY(Estado_Id)
)
GO
CREATE TABLE [dbo].[Localidad](
	[Localidad_Id] [int] IDENTITY(1,1) NOT NULL,
	[Localidad_Nombre] [varchar](150) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
 CONSTRAINT [PK_Localidad] PRIMARY KEY(Localidad_Id),
 CONSTRAINT [FK_Localidad_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id])
)
GO
CREATE TABLE [dbo].[Campus](
	[Campus_Id] [int] IDENTITY(1,1) NOT NULL,
	[Campus_Codigo] [nchar](10) NOT NULL,
	[Campus_Nombre] [varchar](100) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
	[Localidad_Id] [int] NOT NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY(Campus_Id),
 CONSTRAINT [FK_Campus_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id]),
 CONSTRAINT [FK_Campus_Localidad] FOREIGN KEY([Localidad_Id]) REFERENCES [Localidad] ([Localidad_Id])
)
GO

CREATE TABLE [dbo].[Facultad](
	[Facultad_Id] [int] IDENTITY(1,1) NOT NULL,
	[Facultad_Codigo] [nchar](10) NOT NULL,
	[Facultad_Nombre] [varchar](50) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
	[Campus_Id] [int] NOT NULL,
 CONSTRAINT [PK_Facultades_1] PRIMARY KEY(Facultad_Id),
 CONSTRAINT [FK_Facultad_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
 CONSTRAINT [FK_Facultad_Campus] FOREIGN KEY([Campus_Id]) REFERENCES [dbo].[Campus] ([Campus_Id])
 )
GO
CREATE TABLE [dbo].[Escuela](
	[Escuela_Id] [int] IDENTITY(1,1) NOT NULL,
	[Escuela_Codigo] [nchar](10) NOT NULL,
	[Escuela_Nombre] [varchar](100) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
	[Campus_Id] [int] NOT NULL,
	[Facultad_Id] [int] NOT NULL,
 CONSTRAINT [PK_Escuela] PRIMARY KEY(Escuela_Id),
 CONSTRAINT [FK_Escuela_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
 CONSTRAINT [FK_Escuela_Campus] FOREIGN KEY([Campus_Id]) REFERENCES [dbo].[Campus] ([Campus_Id]),
 CONSTRAINT [FK_Escuela_Facultad] FOREIGN KEY([Facultad_Id]) REFERENCES [dbo].[Facultad] ([Facultad_Id])
 ) 
GO
CREATE TABLE [dbo].[Carrera](
	[Carrera_Id] [int] IDENTITY(1,1) NOT NULL,
	[Carrera_Codigo] [nchar](10) NOT NULL,
	[Carrera_Nombre] [varchar](100) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
	[Campus_Id] [int] NOT NULL,
	[Facultad_Id] [int] NOT NULL,
	[Escuela_Id] [int] NOT NULL,
 CONSTRAINT [PK_Carrera] PRIMARY KEY(Carrera_Id),
 CONSTRAINT [FK_Carrera_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
 CONSTRAINT [FK_Carrera_Campus] FOREIGN KEY([Campus_Id]) REFERENCES [dbo].[Campus] ([Campus_Id]),
 CONSTRAINT [FK_Carrera_Facultad] FOREIGN KEY([Facultad_Id]) REFERENCES [dbo].[Facultad] ([Facultad_Id]),
 CONSTRAINT [FK_Carrera_Escuela] FOREIGN KEY([Escuela_Id]) REFERENCES [dbo].[Escuela] ([Escuela_Id])
)
GO
CREATE TABLE [dbo].[Empleado](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Salario] [decimal](13, 2) NULL,
	[FechaIngreso] [date] NOT NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY(Codigo)
 )
GO

CREATE TABLE [dbo].[Usuario](
	[Codigo] [int] NOT NULL,
	[Usuario_Nombre] [varchar](100) NOT NULL,
	[Rol_Id] [int] NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY(Codigo),
 CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
)
GO
CREATE TABLE [dbo].[TipoServicio](
	[TipoServicio_Id] [int] NOT NULL,
	[TipoServicio_Descripcion] [varchar](100) NOT NULL,
	[Estado_Id] [char](1) NOT NULL,
 CONSTRAINT [PK_TipoServicio] PRIMARY KEY(TipoServicio_Id),
 CONSTRAINT [FK_TipoServicio_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
)
GO

CREATE TABLE [dbo].[Servicio](
	[Servicio_Id] [int] IDENTITY(1,1) NOT NULL,
	[Servicio_Codigo] [nchar](10) NOT NULL,
	[Servicio_Descripcion] [varchar](100) NOT NULL,
	[Servicio_FechaInicio] [datetime] NULL,
	[Servicio_FechaCierre] [datetime] NULL,
	[Servicio_Costo] [decimal](12, 2) NULL,
	[UsuarioCodigo] [int] NULL,
	[TipoServicio_Id] [int] NOT NULL,
	[Estado_Id] [char](1) NULL,
	[Campus_Id] [int] NOT NULL,
	[Facultad_Id] [int] NOT NULL,
	[Escuela_Id] [int] NOT NULL,
	[Carrera_Id] [int] NOT NULL,
 CONSTRAINT [PK_Servicio] PRIMARY KEY(Servicio_Id),
 CONSTRAINT [FK_Servicio_Estado] FOREIGN KEY([Estado_Id]) REFERENCES [dbo].[Estado] ([Estado_Id]),
 CONSTRAINT [FK_Servicio_Campus] FOREIGN KEY([Campus_Id]) REFERENCES [dbo].[Campus] ([Campus_Id]),
 CONSTRAINT [FK_Servicio_Carrera] FOREIGN KEY([Carrera_Id]) REFERENCES [dbo].[Carrera] ([Carrera_Id]),
 CONSTRAINT [FK_Servicio_Escuela] FOREIGN KEY([Escuela_Id]) REFERENCES [dbo].[Escuela] ([Escuela_Id])
 
)
GO
CREATE TABLE [dbo].[Profesor](
	[Id] [int] NOT NULL identity(1,1),
	[nombre] [varchar](50) NULL,
	[Estado_Id] [char](1) Not NULL,
 CONSTRAINT [PK_profesor] PRIMARY KEY(Id),
 CONSTRAINT [FK_ProfesorEstado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id])
)
GO

CREATE TABLE [dbo].[adjuntoMaterial](
	[Id] [int] NOT NULL identity(1,1),
	[descripcion] [varchar](50) NOT NULL,
	[ruta] [varchar](100) NULL,
	[Estado_Id] [char](1) Not NULL,
 CONSTRAINT [PK_adjuntoMaterial] PRIMARY KEY(Id),
 CONSTRAINT [FK_adjuntoMaterialEstado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id])

)
GO

CREATE TABLE [dbo].[Modulo](
	[Id] [int] NOT NULL identity(1,1),
	[titulo] [varchar](200) NULL,
	[descripcion] [varchar](500) NULL,
	[fecha_inicio] [datetime] NULL,
	[fecha_fin] [datetime] NULL,
	[id_Profesor] [int] NOT NULL,
	[imagen] [varchar](max) NULL,
	[Estado_Id] [char](1) NOT NULL,
	[id_adjunto] [int] NOT NULL,
	[Servicio_Id] [int] not null,
 CONSTRAINT [PK_modulo] PRIMARY KEY(Id),
 CONSTRAINT [FK_ModuloProfesor] FOREIGN KEY([id_Profesor]) REFERENCES [Profesor] ([Id]),
 CONSTRAINT [FK_ModuloEstado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id]),
 CONSTRAINT [FK_ModuloAdjunto] FOREIGN KEY([id_adjunto]) REFERENCES [adjuntoMaterial] ([Id]),
 CONSTRAINT [FK_Modulo_Servicio] FOREIGN KEY([Servicio_Id]) REFERENCES [Servicio] ([Servicio_Id])
)
GO


CREATE TABLE Estudiante
(
  [Id] [int] not null identity(1,1),
  [Nombre] [varchar](100) not null,
  [Direccion] [varchar](100) null,
  constraint [pk_Estudiante] primary key([Id])
)
GO

CREATE TABLE Calificaciones
(
  Id int not null identity(1,1),
  Modulo_Id int not null,
  Estudiante_Id int not null,
  Calificacion int not null,
  Estado_Id char(1) not null,
  constraint [pk_Calificacion] primary key([Id]),
  CONSTRAINT [FK_CalficaionesModulos] FOREIGN KEY([Modulo_Id]) REFERENCES [Modulo] ([Id]),
  CONSTRAINT [FK_CalificacionEstudiantes] FOREIGN KEY([Estudiante_Id]) REFERENCES [Estudiante] ([Id]),
  CONSTRAINT [FK_CalificacionesEstado] FOREIGN KEY([Estado_Id]) REFERENCES [Estado] ([Estado_Id])
)
GO
CREATE TABLE [dbo].[Usuario](
	[Codigo] [varchar](50) NOT NULL,
	[Contrasena] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY(Codigo)
)
GO
ALTER TABLE [dbo].[Empleado] ADD  CONSTRAINT [DF_Empleado_Salario]  DEFAULT ((0)) FOR [Salario]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Rol_Id]  DEFAULT ((1)) FOR [Rol_Id]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Estado_Id]  DEFAULT ('A') FOR [Estado_Id]
GO
ALTER TABLE [dbo].[Servicio] ADD  CONSTRAINT [DF_Servicio_Servicio_FechaInicio]  DEFAULT ('') FOR [Servicio_FechaInicio]
GO
ALTER TABLE [dbo].[Servicio] ADD  CONSTRAINT [DF_Servicio_Servicio_FechaCierre]  DEFAULT ('') FOR [Servicio_FechaCierre]
GO
ALTER TABLE [dbo].[Servicio] ADD  CONSTRAINT [DF_Servicio_Servicio_Costo]  DEFAULT ((0)) FOR [Servicio_Costo]
GO
ALTER TABLE [dbo].[Servicio] ADD  CONSTRAINT [DF_Servicio_Estado_Id]  DEFAULT ('I') FOR [Estado_Id]
GO
ALTER TABLE [dbo].[TipoServicio] ADD  CONSTRAINT [DF_TipoServicio_Estado_Id]  DEFAULT ('A') FOR [Estado_Id]
GO





SET IDENTITY_INSERT [dbo].[Rol] ON 
GO
INSERT [dbo].[Rol] ([Id], [Descripcion]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Rol] ([Id], [Descripcion]) VALUES (2, N'Estudiante')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF

GO
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'A', N'Activo')
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'C', N'Cerrado')
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'I', N'Inactivo')
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'P', N'En Proceso')
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'E', N'Pendiente')
INSERT [dbo].[Estado] ([Estado_Id], [Estado_Nombre]) VALUES (N'N', N'Inscrito')
GO
SET IDENTITY_INSERT [dbo].[Localidad] ON 
INSERT [dbo].[Localidad] ([Localidad_Id], [Localidad_Nombre], [Estado_Id]) VALUES (1, N'Distrito Nacional', N'A')
SET IDENTITY_INSERT [dbo].[Localidad] OFF
GO
SET IDENTITY_INSERT [dbo].[Campus] ON 
INSERT [dbo].[Campus] ([Campus_Id], [Campus_Codigo], [Campus_Nombre], [Estado_Id], [Localidad_Id]) VALUES (1, N'SEDE      ', N'Sede Central', N'A', 1)
SET IDENTITY_INSERT [dbo].[Campus] OFF
GO
INSERT [dbo].[Usuario] ([Codigo], [Usuario_Nombre], [RolId], [Estado_Id]) VALUES (1, N'Juan Manuel Feliz', 2, N'A')
INSERT [dbo].[Usuario] ([Codigo], [Usuario_Nombre], [RolId], [Estado_Id]) VALUES (2, N'Eddy Brito', 1, N'A')
INSERT [dbo].[Usuario] ([Codigo], [Usuario_Nombre], [RolId], [Estado_Id]) VALUES (3, N'Carlos Caraballo', 1, N'A')
INSERT [dbo].[Usuario] ([Codigo], [Usuario_Nombre], [RolId], [Estado_Id]) VALUES (4, N'Delgado Bello', 1, N'A')
INSERT [dbo].[Usuario] ([Codigo], [Usuario_Nombre], [RolId], [Estado_Id]) VALUES (5, N'Martha Perez', 1, N'A')
GO

INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (1, N'Juan Manuel Feliz', CAST(0.00 AS Decimal(13, 2)), CAST(N'1998-06-01' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (2, N'Dominga Gonzalez', CAST(45000.00 AS Decimal(13, 2)), CAST(N'2015-02-01' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (3, N'Erich T. Sherman', CAST(37729.00 AS Decimal(13, 2)), CAST(N'2121-03-01' AS Date))

INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (54, N'Daphne R. Mckay', CAST(69135.00 AS Decimal(13, 2)), CAST(N'2020-08-03' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (55, N'Aladdin O. Newman', CAST(22945.00 AS Decimal(13, 2)), CAST(N'2121-08-26' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (56, N'Ryder Y. Rice', CAST(94884.00 AS Decimal(13, 2)), CAST(N'2121-01-31' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (57, N'Alfonso B. Guy', CAST(30001.00 AS Decimal(13, 2)), CAST(N'2020-07-30' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (58, N'Keegan G. Wyatt', CAST(90143.00 AS Decimal(13, 2)), CAST(N'2121-11-12' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (59, N'Athena G. Mcintosh', CAST(42324.00 AS Decimal(13, 2)), CAST(N'2121-01-03' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (60, N'Nissim E. Small', CAST(58505.00 AS Decimal(13, 2)), CAST(N'2020-07-28' AS Date))
INSERT [dbo].[Empleado] ([Codigo], [Nombre], [Salario], [FechaIngreso]) VALUES (61, N'Malik L. Wolfe', CAST(60507.00 AS Decimal(13, 2)), CAST(N'2020-12-03' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Facultad] ON 

INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (1, N'FC00001   ', N'Facultad de Ciencias', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (2, N'FC00002   ', N'Facultad de Ciencias Agron�micas y Veterinarias', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (3, N'FC00003   ', N'Facultad de Ciencias Agron�micas y Veterinarias', N'A', 2)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (4, N'FI00004   ', N'Facultad de Ingenier�a y Arquitectura', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (5, N'FC00005   ', N'Facultad de Ciencias Econ�micas y Sociales', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (6, N'FC00006   ', N'Facultad de Ciencias de la Salud', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (7, N'FH00007   ', N'Facultad de Humanidades', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (8, N'FA00008   ', N'Facultad de Artes', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (9, N'CJ00009   ', N'Facultad de Ciencias Jur�dicas y Pol�ticas', N'A', 1)
INSERT [dbo].[Facultad] ([Facultad_Id], [Facultad_Codigo], [Facultad_Nombre], [Estado_id], [Campus_Id]) VALUES (10, N'FC00010   ', N'Facultad de Educaci�n', N'A', 1)
SET IDENTITY_INSERT [dbo].[Facultad] OFF
go
SET IDENTITY_INSERT [dbo].[Escuela] ON 

INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (1, N'BIO1010   ', N'Biolog�a', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (2, N'CGO1020   ', N'Ciencias Geogr�ficas', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (3, N'FIS1030   ', N'F�sica', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (4, N'MAT1040   ', N'Matem�tica', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (5, N'MBP1050   ', N'Micro biolog�a y Parasitolog�a', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (6, N'QUI1070   ', N'Qu�mica', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (7, N'INF1060   ', N'Inform�tica', N'A', 1, 1)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (8, N'AGR2010   ', N'Agronom�a', N'A', 1, 2)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (9, N'ZOO2020   ', N'Zootecnia', N'A', 1, 2)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (10, N'VET2030   ', N'Veterinaria', N'A', 1, 2)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (11, N'AGR3010   ', N'Agronom�a', N'A', 1, 3)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (12, N'ZOO3020   ', N'Zootecnia', N'A', 1, 3)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (13, N'VET3030   ', N'Veterinaria', N'A', 1, 3)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (14, N'ARQ4010   ', N'Arquitectura', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (15, N'AGR4020   ', N'Agrimensura', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (16, N'ICI4030   ', N'Ingenier�a Civil', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (17, N'IEL4040   ', N'Ingenier�a Electromec�nica', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (18, N'IQU4050   ', N'Ingenier�a Qu�mica', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (19, N'IIN4060   ', N'Ingenier�a Industrial', N'A', 1, 4)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (20, N'ADM5010   ', N'Administraci�n', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (21, N'CON5020   ', N'Contabilidad', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (22, N'EST5030   ', N'Estad�stica', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (23, N'ECO5040   ', N'Econom�a', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (24, N'SOC5050   ', N'Sociolog�a', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (25, N'MEC5060   ', N'Mercadotecnia', N'A', 1, 5)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (26, N'BIO6010   ', N'Bioan�lisis', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (27, N'FAR6020   ', N'Farmacia', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (28, N'ENF6030   ', N'Enfermer�a', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (29, N'ODO6040   ', N'Odontolog�a', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (30, N'MED6050   ', N'Medicina', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (31, N'SPU6060   ', N'Salud P�blica', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (32, N'CFI6070   ', N'Ciencias Fisiol�gicas', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (33, N'CMO6080   ', N'Ciencias Morfol�gicas', N'A', 1, 6)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (34, N'CSO7010   ', N'Comunicaci�n Social', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (35, N'FIL7020   ', N'Filosof�a', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (36, N'IDI7030   ', N'Idioma', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (37, N'LET7040   ', N'Letras', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (38, N'PED7050   ', N'Pedagog�a', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (39, N'PSI7060   ', N'Psicolog�a', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (40, N'OPR7070   ', N'Orientaci�n Profesional', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (41, N'EID7080   ', N'Escuela de Idiomas', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (42, N'HAN7090   ', N'Historia y Antropolog�a', N'A', 1, 7)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (43, N'CHA8010   ', N'Cr�tica e Historia del Arte', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (44, N'PUB8020   ', N'Publicidad', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (45, N'TEA8030   ', N'Teatro', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (46, N'MUS8040   ', N'M�sica', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (47, N'CTF8050   ', N'Cine TV-Fotograf�a', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (48, N'DIM8060   ', N'Dise�o Industrial y Moda', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (49, N'APL8070   ', N'Artes Pl�sticas', N'A', 1, 8)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (50, N'DER9010   ', N'Derecho', N'A', 1, 9)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (51, N'CPO9020   ', N'Ciencias Pol�ticas', N'A', 1, 9)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (52, N'CRI9030   ', N'Criminolog�a', N'A', 1, 9)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (53, N'TGE1110   ', N'Teor�a y Gesti�n Educativa', N'A', 1, 10)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (54, N'EIF1120   ', N'Educaci�n Infantil y B�sicas', N'A', 1, 10)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (55, N'EME1130   ', N'Educaci�n Media', N'A', 1, 10)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (56, N'OPE1140   ', N'Orientaci�n y Pedagog�a', N'A', 1, 10)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (57, N'FDE1150   ', N'F�sica Y Deporte', N'A', 1, 10)
INSERT [dbo].[Escuela] ([Escuela_Id], [Escuela_Codigo], [Escuela_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id]) VALUES (58, N'BIB1160   ', N'Bibliotecolog�a', N'A', 1, 10)
SET IDENTITY_INSERT [dbo].[Escuela] OFF

GO
SET IDENTITY_INSERT [dbo].[Carrera] ON 

INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (1, N'30401     ', N'Licenciatura en Ciencias de la Comunicaci�n Social', N'A', 1, 7, 34)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (2, N'30403     ', N'Licenciatura en Comunicaci�n Social Menci�n Periodismo', N'A', 1, 7, 34)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (3, N'30404     ', N'Licenciatura en Comunicaci�n Social Menci�n Relaciones P�blicas', N'A', 1, 7, 34)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (4, N'30405     ', N'Licenciatura en Comunicaci�n Social Menci�n Comunicaci�n Gr�fica', N'A', 1, 7, 34)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (5, N'40201     ', N'Licenciatura en Biolog�a ', N'A', 1, 1, 1)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (6, N'10201     ', N'Licenciatura en Geograf�a', N'A', 1, 1, 2)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (7, N'10202     ', N'Licenciatura en Geograf�a  Menci�n Recursos Naturales', N'A', 1, 1, 2)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (8, N'10203     ', N'Licenciatura en Geograf�a Menci�n Representaci�n Espacial', N'A', 1, 1, 2)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (9, N'10301     ', N'LICENCIATURA EN F�SICA (Pensum)', N'A', 1, 1, 3)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (10, N'10401     ', N'LICENCIATURA EN MATEM�TICAS (Pensum)', N'A', 1, 1, 4)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (11, N'10501     ', N'Licenciatura en Microbiolog�a (Pensum)', N'A', 1, 1, 5)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (12, N'10601     ', N'Licenciatura en Qu�mica (Pensum)', N'A', 1, 1, 6)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (13, N'10602     ', N'TECN�LOGO SUPERIOR EN ALIMENTOS (PENSUM)', N'A', 1, 1, 6)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (14, N'10701     ', N'Licenciatura en Inform�tica (Pensum)', N'A', 1, 1, 7)
INSERT [dbo].[Carrera] ([Carrera_Id], [Carrera_Codigo], [Carrera_Nombre], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id]) VALUES (15, N'10702     ', N'T�CNICO EN INFRAESTRUCTURA DE TIC.', N'A', 1, 1, 7)
SET IDENTITY_INSERT [dbo].[Carrera] OFF
GO
GO

SET IDENTITY_INSERT [dbo].[Servicio] ON 

INSERT [dbo].[Servicio] ([Servicio_Id], [Servicio_Codigo], [Servicio_Descripcion], [Servicio_FechaInicio], [Servicio_FechaCierre], [Servicio_Costo], [UsuarioCodigo], [TipoServicio_Id], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id], [Carrera_Id]) VALUES (1, N'MSEDE#11  ', N'Monogr�fico de Inform�tica SEDE #11', CAST(N'2010-08-08T00:00:00.000' AS DateTime), CAST(N'2010-12-23T00:00:00.000' AS DateTime), CAST(12000.00 AS Decimal(12, 2)), 3, 2, N'I', 1, 1, 7, 14)
INSERT [dbo].[Servicio] ([Servicio_Id], [Servicio_Codigo], [Servicio_Descripcion], [Servicio_FechaInicio], [Servicio_FechaCierre], [Servicio_Costo], [UsuarioCodigo], [TipoServicio_Id], [Estado_Id], [Campus_Id], [Facultad_Id], [Escuela_Id], [Carrera_Id]) VALUES (2, N'MSEDE#12  ', N'Tesis de Biolog�a SEDE #12', CAST(N'2010-09-02T00:00:00.000' AS DateTime), CAST(N'2010-12-23T00:00:00.000' AS DateTime), CAST(12000.00 AS Decimal(12, 2)), 2, 1, N'I', 1, 1, 1, 5)
SET IDENTITY_INSERT [dbo].[Servicio] OFF
GO
INSERT [dbo].[TipoServicios] ([TipoServicio_Id], [TipoServicio_Descripcion], [Estado_Id]) VALUES (1, N'Tesis de Grado', N'A')
INSERT [dbo].[TipoServicios] ([TipoServicio_Id], [TipoServicio_Descripcion], [Estado_Id]) VALUES (2, N'Monográfico', N'A')
GO
INSERT [dbo].[Usuario] ([Codigo], [Contrasena]) VALUES (N'1', N'12345')
GO
