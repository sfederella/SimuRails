USE [master]
GO
/****** Object:  Database [SimuRails_DB]    Script Date: 24/7/2017 8:53:56 p. m. ******/
CREATE DATABASE [SimuRails_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimuRails_DB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLSERVER2012\MSSQL\DATA\SimuRails_DB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SimuRails_DB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLSERVER2012\MSSQL\DATA\SimuRails_DB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SimuRails_DB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimuRails_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimuRails_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimuRails_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimuRails_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimuRails_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimuRails_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimuRails_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimuRails_DB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SimuRails_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimuRails_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimuRails_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimuRails_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimuRails_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimuRails_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimuRails_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimuRails_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimuRails_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimuRails_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimuRails_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimuRails_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimuRails_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimuRails_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimuRails_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimuRails_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimuRails_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimuRails_DB] SET  MULTI_USER 
GO
ALTER DATABASE [SimuRails_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimuRails_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimuRails_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimuRails_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SimuRails_DB]
GO
/****** Object:  Table [dbo].[Coche]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Coche](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modelo] [varchar](100) NOT NULL,
	[EsLocomotora] [bit] NULL,
	[TipoConsumo] [int] NULL,
	[ConsumoMovimiento] [int] NULL,
	[ConsumoParado] [int] NULL,
	[CantidadAsientos] [int] NOT NULL,
	[MaximoLegalPasajeros] [int] NOT NULL,
	[CapacidadMaximaPasajeros] [int] NOT NULL,
 CONSTRAINT [PK_Coches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estacion]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[PersonasEsperandoMin] [int] NOT NULL,
	[PersonasEsperandoMax] [int] NOT NULL,
	[TipoFDP] [int] NOT NULL,
 CONSTRAINT [PK_Estaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estacion_X_Incidente]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estacion_X_Incidente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Incidente] [int] NOT NULL,
	[Id_Estacion] [int] NOT NULL,
 CONSTRAINT [PK_Estaciones_X_Incidentes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Formacion]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Formacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Formaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Formacion_X_Coche]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formacion_X_Coche](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Formacion] [int] NOT NULL,
	[Id_Coche] [int] NOT NULL,
	[VecesRepetido] [int] NOT NULL,
 CONSTRAINT [PK_Formaciones_X_Coches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Incidente]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Incidente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[ProbabilidadOcurrencia] [int] NOT NULL,
	[TiempoDemora] [int] NOT NULL,
 CONSTRAINT [PK_Incidentes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Servicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Id_Estacion_Mantenimiento] [int] NULL,
	[ProgramacionStr] [nvarchar](max) NULL,
 CONSTRAINT [PK_Servicios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio_X_Formacion]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio_X_Formacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Servicio] [int] NOT NULL,
	[Id_Formacion] [int] NOT NULL,
	[CantFormaciones] [int] NULL,
 CONSTRAINT [PK_Servicios_X_Formaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Simulacion]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Simulacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TiempoInicial] [int] NOT NULL,
	[TiempoFinal] [int] NOT NULL,
	[Id_Traza] [int] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Simulaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tramo]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tramo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Estacion_Anterior] [int] NOT NULL,
	[Id_Estacion_Siguiente] [int] NOT NULL,
	[Id_Servicio] [int] NOT NULL,
	[Distancia] [int] NOT NULL,
	[TiempoViaje] [int] NOT NULL,
	[EstSigEsParada] [bit] NOT NULL,
 CONSTRAINT [PK_Relaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Traza]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Traza](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Trazas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Traza_X_Servicio]    Script Date: 24/7/2017 8:53:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traza_X_Servicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Servicio] [int] NOT NULL,
	[Id_Traza] [int] NOT NULL,
 CONSTRAINT [PK_Trazas_X_Servicios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Coche] ON 

INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (1055, N'Loc. General Motors-EMD G22', 1, 1, 300, 50, 50, 80, 130)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (1056, N'Coche General Motors-EMD G10', 0, 0, 0, 0, 60, 80, 130)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (1057, N'Loc. General Motors-EMD E80', 1, 0, 4, 1, 50, 80, 130)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (1058, N'Coche General Motors-EMD E83', 0, 0, 0, 0, 60, 80, 130)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (2069, N'Coche CMF1021', 0, 0, 0, 0, 65, 100, 200)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (2070, N'Loc. CMF1021', 1, 1, 2000, 200, 65, 100, 200)
INSERT [dbo].[Coche] ([Id], [Modelo], [EsLocomotora], [TipoConsumo], [ConsumoMovimiento], [ConsumoParado], [CantidadAsientos], [MaximoLegalPasajeros], [CapacidadMaximaPasajeros]) VALUES (2071, N'Camion de Carga', 1, 0, 50, 1, 2, 2, 2)
SET IDENTITY_INSERT [dbo].[Coche] OFF
SET IDENTITY_INSERT [dbo].[Estacion] ON 

INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (16, N'Once', 100, 1000, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (17, N'Caballito', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (18, N'Flores', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (19, N'Floresta', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (20, N'Villa Luro', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (21, N'Liniers', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (22, N'Ciudadela', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (23, N'Ramos Mejia', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (24, N'Haedo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (25, N'Moron', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (26, N'Castelar', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (27, N'Ituzaingó', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (28, N'San Antorio De Padua', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (29, N'Merlo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (30, N'Paso del Rey', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (31, N'Moreno', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (32, N'Retiro', 221, 411, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (33, N'Saldías', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (34, N'Raul Scalabrini Ortiz', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (35, N'Aristobulo del Valle', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (36, N'M. M. Padilla', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (37, N'Florida', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (38, N'Munro', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (39, N'Carapachay', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (40, N'Villa Adelina', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (41, N'Boulogne Sur Mer', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (42, N'Vice A. Montes', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (43, N'Don Torcuato', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (44, N'Adolfo Sourdeaux', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (45, N'Villa de Mayo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (46, N'Los Polvorines', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (47, N'Ing. Pablo Nogués', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (48, N'Grand Bourg', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (49, N'Tierras Altas', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (50, N'Tortuguitas', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (51, N'Alberti', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (52, N'Del Viso', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (53, N'Villa Rosa', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (54, N'La Reja', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (55, N'Km 34.5', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (56, N'Ferrari', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (57, N'Mariano Acosta', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (58, N'Marcos Paz', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (59, N'Zamudio', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (60, N'Hornos', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (61, N'Las Heras', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (62, N'Speratti', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (63, N'Zapiola', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (64, N'Empalme Lobos', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (65, N'Lobos', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (66, N'Francisco Alvarez', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (67, N'Pablo Marín', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (68, N'Las Malvinas', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (69, N'General Rodriguez', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (70, N'La Fraternidad', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (71, N'Lezica y Torrezuri', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (72, N'Universidad de Luján', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (73, N'Luján', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (74, N'Jáuregui', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (75, N'Olivera', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (76, N'Gowland', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (77, N'Mercedes', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (78, N'Federico Lacroze', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (79, N'Artigas', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (80, N'Francisco Beiro', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (81, N'El Libertador', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (82, N'Devoto', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (83, N'Lynch', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (84, N'F. Moreno', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (85, N'Lourdes', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (86, N'Tropezón', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (87, N'J.M.Bosch', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (88, N'Martin Coronado', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (89, N'Pablo Podestá', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (90, N'Jorge Newbery', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (91, N'Rubén Dario', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (92, N'Ejercito de los Andes', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (93, N'Lasalle', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (94, N'Sgto. Barrufaldi', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (95, N'Capitán Lozano', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (96, N'Teniente Agneta', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (98, N'Sgto. Cabral', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (99, N'Gral. Lemos', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1123, N'Retiro', 221, 411, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1124, N'3 de Febrero', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1125, N'Carranza', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1126, N'Colegiales', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1127, N'Belgrano R', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1128, N'Coghlan', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1129, N'Saavedra', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1130, N'Juan B. Justo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1131, N'Florida', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1132, N'Cetrángolo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1133, N'Mitre', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1134, N'Drago', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1135, N'Urquiza', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1136, N'Pueyrredón', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1137, N'Miguelete', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1138, N'San Martín', 50, 300, 0)
GO
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1139, N'San Andrés', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1140, N'Malaver', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1141, N'Villa Ballester', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1142, N'Chilavert', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1143, N'José Leon Suarez', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1144, N'Bancalari', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1145, N'Pacheco', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1146, N'Benavidez', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1147, N'Maschwizt', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1148, N'Escobar', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1149, N'Río Lujan', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1150, N'Otamendi', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1151, N'Campana', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1152, N'Km. 83', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1153, N'Zárate', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1154, N'L. de la Torre', 12, 23, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1155, N'Belgrano C', 73, 136, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1156, N'Nuñez', 16, 29, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1157, N'Rivadavia', 15, 28, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1158, N'Vicente López', 18, 33, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1159, N'Olivos', 20, 38, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1160, N'La Lucila', 14, 26, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1161, N'Martínez', 33, 61, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1162, N'Acassuso', 14, 27, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1163, N'San Isidro', 58, 107, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1164, N'Beccar', 16, 30, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1165, N'Victoria', 17, 31, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1166, N'Virreyes', 23, 43, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1167, N'San Fernando', 26, 49, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1168, N'Carupá', 22, 41, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1169, N'Tigre', 66, 122, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1170, N'Schweitzer', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1171, N'El Talar', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1172, N'López Camelo', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1173, N'Garín', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1174, N'Maq. Savio', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1175, N'Matheu', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1176, N'Zelaya', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1178, N'Los Cardales', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1179, N'Capilla del Señor', 50, 300, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1180, N'Parada Rosario', 0, 1, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1181, N'Parada Córdoba', 0, 1, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1182, N'Parada Tucuman', 0, 1, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1183, N'Parada Salta', 0, 1, 0)
INSERT [dbo].[Estacion] ([Id], [Nombre], [PersonasEsperandoMin], [PersonasEsperandoMax], [TipoFDP]) VALUES (1184, N'Parada Buenos Aires', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[Estacion] OFF
SET IDENTITY_INSERT [dbo].[Estacion_X_Incidente] ON 

INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (137, 12, 92)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (140, 52, 39)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (141, 51, 39)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (142, 12, 39)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1169, 12, 17)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1170, 41, 17)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1173, 48, 18)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1178, 52, 16)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1179, 51, 16)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1181, 1073, 1180)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1184, 12, 1128)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1185, 12, 1129)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1193, 51, 1123)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1194, 39, 1123)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1195, 51, 32)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1196, 39, 32)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1197, 12, 1155)
INSERT [dbo].[Estacion_X_Incidente] ([Id], [Id_Incidente], [Id_Estacion]) VALUES (1198, 48, 1155)
SET IDENTITY_INSERT [dbo].[Estacion_X_Incidente] OFF
SET IDENTITY_INSERT [dbo].[Formacion] ON 

INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (49, N'Form. Elec. Mitre')
INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (1052, N'Form. Diesel Mitre')
INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (1053, N'Form. Elec. Sarmiento')
INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (1054, N'Form. Elec. China Belgrano Sur')
INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (1055, N'Form.  Elec. China Mitre')
INSERT [dbo].[Formacion] ([Id], [Nombre]) VALUES (1056, N'Transporte Camión')
SET IDENTITY_INSERT [dbo].[Formacion] OFF
SET IDENTITY_INSERT [dbo].[Formacion_X_Coche] ON 

INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1095, 49, 1055, 1)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1097, 1052, 1057, 1)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1099, 49, 1056, 5)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1100, 1053, 1055, 1)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1101, 1053, 1056, 8)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1102, 1052, 1058, 5)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1103, 1054, 2070, 1)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1104, 1054, 2069, 7)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1105, 1055, 2070, 1)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1106, 1055, 2069, 5)
INSERT [dbo].[Formacion_X_Coche] ([Id], [Id_Formacion], [Id_Coche], [VecesRepetido]) VALUES (1107, 1056, 2071, 1)
SET IDENTITY_INSERT [dbo].[Formacion_X_Coche] OFF
SET IDENTITY_INSERT [dbo].[Incidente] ON 

INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (5, N'Piquete', N'Gente se manifiesta sobre las vias prohibiendo el paso de la formación', 20, 30)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (8, N'Paro Medio', N'Paro Sindical', 5, 600)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (12, N'Inundacion', N'Inundacion de ciertas estaciones', 20, 120)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (36, N'Via Cortada Leve', N'Tiene en cuenta aquellos cortes que son simples de solucionar, por ejemplo que haya basura en las vias o algun otro objeto que provoque su obstrución.', 70, 30)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (37, N'Via Cortada Medio', N'Tiene en cuenta aquellos cortes en donde los objetos que obstruen las vias son mas grandes y complejos de mover porque se necesita maquinaria especializada, por ejemplo un auto o algún vehículo de transporte.', 30, 100)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (38, N'Via Cortada Grave', N'Tiene en cuenta aquellos cortes que son mas graves y menos frecuentes, por ejemplo un accidente automovilistico.', 10, 240)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (39, N'Paro Grave', N'Paro Sindical agravado por el tiempo que duran las negociaciones, por ejemplo reincorporacion de trabajadores despedidos', 2, 2880)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (41, N'Mantenimiento Estación', N'Se suspende el servicio por mantenimientos varios en la estación', 100, 240)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (42, N'Mantenimiento Vias', N'Se suspende el servicio por mantenimiento de las vias', 10, 180)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (48, N'Obras Sobre Vias', N'Se realizan reparaciones sobre vias', 30, 360)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (51, N'Descarrilamiento de Formaciones', N'descarrilamiento de uno o mas coches de la formacion', 2, 999999999)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (52, N'Colision de Formaciones', N'Choque de formaciones frontal ', 2, 999999999)
INSERT [dbo].[Incidente] ([Id], [Nombre], [Descripcion], [ProbabilidadOcurrencia], [TiempoDemora]) VALUES (1073, N'Cubierta Rota', N'Se pincha una llanta dal camión', 10, 200)
SET IDENTITY_INSERT [dbo].[Incidente] OFF
SET IDENTITY_INSERT [dbo].[Servicio] ON 

INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (10, N'Once Moreno', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (16, N'Moreno Mercedes', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (17, N'Moreno U. de Luján Mercedes', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (18, N'Merlo Lobos', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (19, N'Merlo Hornos Lobos', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (20, N'Once Castelar Moreno', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (21, N'Caballito Castelar Moreno', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (22, N'Retiro Villa Rosa', NULL, NULL)
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1025, N'Retiro Tigre', NULL, N'04:57;05:12;05:41;05:55;06:24;06:39;07:08;07:23;07:52;08:06;08:35;08:50;09:19;09:34;10:03;10:17;10:46;11:01;11:30;11:45;12:14;12:28;12:57;13:12;13:41;13:56;14:25;14:39;15:08;15:23;15:52;16:07;16:36;16:50;17:19;17:34;18:03;18:18;18:47;19:09;19:30;19:45;20:14;20:29;20:58;21:12;21:41;21:56;22:25;22:40;23:09;23:23')
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1026, N'Retiro José León Suarez', NULL, N'05:22;05:39;06:12;06:30;07:05;07:22;07:56;08:12;08:50;09:06;09:39;09:56;10:30;10:48;11:22;11:39;12:12;12:30;13:05;13:23;13:56;14:12;14:48;15:05;15:39;15:56;16:30;16:48;17:21;17:39;18:12;18:30;19:05;19:21;19:54;20:12;20:48;21:05;21:39;21:56;22:30;22:48;23:22')
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1027, N'Retiro Mitre', NULL, N'05:45;06:15;07:15;07:45;08:45;09:15;10:15;10:45;11:45;12:15;13:15;13:45;14:45;15:15;16:15;16:45;17:45;18:15;19:15;19:45;20:45;21:15')
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1028, N'Victoria Capilla del Señor', NULL, N'03:02;07:40;13:56;16:17;21:35;23:16')
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1029, N'Villa Ballester Zárate', NULL, N'02:10;18:30')
INSERT [dbo].[Servicio] ([Id], [Nombre], [Id_Estacion_Mantenimiento], [ProgramacionStr]) VALUES (1030, N'Ruta Camión', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Servicio] OFF
SET IDENTITY_INSERT [dbo].[Servicio_X_Formacion] ON 

INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (16, 19, 49, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1023, 1025, 49, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1024, 1026, 49, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1025, 1027, 49, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1028, 1028, 1052, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1029, 1029, 1052, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1030, 21, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1031, 16, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1032, 17, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1033, 20, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1034, 10, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1035, 18, 1053, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1037, 22, 1054, NULL)
INSERT [dbo].[Servicio_X_Formacion] ([Id], [Id_Servicio], [Id_Formacion], [CantFormaciones]) VALUES (1038, 1030, 1056, NULL)
SET IDENTITY_INSERT [dbo].[Servicio_X_Formacion] OFF
SET IDENTITY_INSERT [dbo].[Simulacion] ON 

INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1022, 0, 24, 1044, N'Sim. Mitre')
INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1023, 0, 24, 33, N'Sarmiento Once 2012')
INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1024, 0, 24, 1045, N'Sim. Sarmiento')
INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1025, 0, 4, 1046, N'Sim. Camión de Cacho')
INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1026, 0, 24, 1047, N'Sim. Sarmiento Único Servicio')
INSERT [dbo].[Simulacion] ([Id], [TiempoInicial], [TiempoFinal], [Id_Traza], [Nombre]) VALUES (1027, 0, 24, 34, N'Sim. Belgrano Norte')
SET IDENTITY_INSERT [dbo].[Simulacion] OFF
SET IDENTITY_INSERT [dbo].[Tramo] ON 

INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (7, 16, 17, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (8, 17, 18, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (9, 18, 19, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (10, 19, 20, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (11, 20, 21, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (12, 21, 22, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (13, 22, 23, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (14, 23, 24, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (15, 24, 25, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (16, 25, 26, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (17, 26, 27, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (18, 27, 28, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (19, 28, 29, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (20, 29, 30, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (21, 30, 31, 10, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (35, 31, 54, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (36, 54, 66, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (37, 66, 67, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (38, 67, 68, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (39, 68, 69, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (40, 69, 70, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (41, 70, 71, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (42, 71, 72, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (43, 72, 73, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (44, 73, 74, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (45, 74, 75, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (46, 75, 76, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (47, 76, 77, 16, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (48, 31, 72, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (49, 72, 73, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (50, 73, 74, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (51, 74, 75, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (52, 75, 76, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (53, 76, 77, 17, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (54, 29, 55, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (55, 55, 56, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (56, 56, 57, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (57, 57, 58, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (58, 58, 59, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (59, 59, 60, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (60, 60, 61, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (61, 61, 62, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (62, 62, 63, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (63, 63, 64, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (64, 64, 65, 18, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (65, 29, 60, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (66, 60, 61, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (67, 61, 62, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (68, 62, 63, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (69, 63, 64, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (70, 64, 65, 19, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (71, 16, 26, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (72, 26, 27, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (73, 27, 28, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (74, 28, 29, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (75, 29, 30, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (76, 30, 31, 20, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (77, 17, 26, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (78, 26, 27, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (79, 27, 28, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (80, 28, 29, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (81, 29, 30, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (82, 30, 31, 21, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (83, 32, 33, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (84, 33, 34, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (85, 34, 35, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (86, 35, 36, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (87, 36, 37, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (88, 37, 38, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (89, 38, 39, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (90, 39, 40, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (91, 40, 41, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (92, 41, 42, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (93, 42, 43, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (94, 43, 44, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (95, 44, 45, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (96, 45, 46, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (97, 46, 47, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (98, 47, 48, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (99, 48, 49, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (100, 49, 50, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (101, 50, 51, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (102, 51, 52, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (103, 52, 53, 22, 5, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1164, 1184, 1180, 1030, 315, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1165, 1180, 1181, 1030, 405, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1166, 1181, 1182, 1030, 561, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1167, 1182, 1183, 1030, 304, 0, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1186, 1123, 1154, 1025, 7, 11, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1187, 1154, 1155, 1025, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1188, 1155, 1156, 1025, 2, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1189, 1156, 1157, 1025, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1190, 1157, 1158, 1025, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1191, 1158, 1159, 1025, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1192, 1159, 1160, 1025, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1193, 1160, 1161, 1025, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1194, 1161, 1162, 1025, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1195, 1162, 1163, 1025, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1196, 1163, 1164, 1025, 2, 3, 1)
GO
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1197, 1164, 1165, 1025, 2, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1198, 1165, 1166, 1025, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1199, 1166, 1167, 1025, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1200, 1167, 1168, 1025, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1201, 1168, 1169, 1025, 2, 5, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1211, 1165, 1170, 1028, 4, 9, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1212, 1170, 1171, 1028, 7, 25, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1213, 1171, 1172, 1028, 3, 9, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1214, 1172, 1173, 1028, 6, 15, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1215, 1173, 1174, 1028, 4, 9, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1216, 1174, 1175, 1028, 6, 13, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1217, 1175, 1176, 1028, 4, 7, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1218, 1176, 1178, 1028, 12, 18, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1219, 1178, 1179, 1028, 11, 16, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1220, 1141, 1144, 1029, 9, 27, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1221, 1144, 1145, 1029, 3, 6, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1222, 1145, 1146, 1029, 8, 10, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1223, 1146, 1147, 1029, 5, 18, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1224, 1147, 1148, 1029, 7, 10, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1225, 1148, 1149, 1029, 19, 16, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1226, 1149, 1150, 1029, 6, 8, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1227, 1150, 1151, 1029, 10, 10, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1228, 1151, 1152, 1029, 2, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1229, 1152, 1153, 1029, 10, 14, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1230, 1123, 1124, 1027, 6, 8, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1231, 1124, 1125, 1027, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1232, 1125, 1126, 1027, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1233, 1126, 1127, 1027, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1234, 1127, 1128, 1027, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1235, 1128, 1129, 1027, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1236, 1129, 1130, 1027, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1237, 1130, 1131, 1027, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1238, 1131, 1132, 1027, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1239, 1132, 1133, 1027, 1, 5, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1240, 1123, 1124, 1026, 6, 8, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1241, 1125, 1126, 1026, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1242, 1126, 1127, 1026, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1243, 1127, 1134, 1026, 2, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1244, 1124, 1125, 1026, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1245, 1134, 1135, 1026, 1, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1246, 1135, 1136, 1026, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1247, 1136, 1137, 1026, 1, 5, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1248, 1137, 1138, 1026, 2, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1249, 1138, 1139, 1026, 1, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1250, 1139, 1140, 1026, 1, 3, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1251, 1140, 1141, 1026, 1, 4, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1252, 1141, 1142, 1026, 1, 2, 1)
INSERT [dbo].[Tramo] ([Id], [Id_Estacion_Anterior], [Id_Estacion_Siguiente], [Id_Servicio], [Distancia], [TiempoViaje], [EstSigEsParada]) VALUES (1253, 1142, 1143, 1026, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[Tramo] OFF
SET IDENTITY_INSERT [dbo].[Traza] ON 

INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (31, N'Sarmiento (Ramal Merlo-Lobos)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (33, N'Sarmiento (Ramal Once-Moreno)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (34, N'Belgrano Norte (Ramal Retiro - Villa Rosa)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (36, N'Sarmiento (Ramal Moreno-Mercedes)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1040, N'Mitre (Ramal Victoria-Capilla Del Señor)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1042, N'Mitre (Ramal Retiro-Tigre, Mitre, J.L.Suarez)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1043, N'Mitre (Ramal Villa Ballester-Zárate)')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1044, N'Mitre Completo')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1045, N'Sarmiento Completo')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1046, N'Recorrido Camión')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1047, N'Sarmiento (Once Moreno) Unico Servicio')
INSERT [dbo].[Traza] ([Id], [Nombre]) VALUES (1048, N'Mitre (Ramal Retiro - Tigre)')
SET IDENTITY_INSERT [dbo].[Traza] OFF
SET IDENTITY_INSERT [dbo].[Traza_X_Servicio] ON 

INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1114, 1026, 1042)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1115, 1027, 1042)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1116, 1025, 1042)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1118, 1028, 1040)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1119, 1029, 1043)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1120, 1026, 1044)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1121, 1027, 1044)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1122, 1025, 1044)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1123, 1028, 1044)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1124, 1029, 1044)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1125, 19, 31)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1126, 18, 31)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1127, 16, 36)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1128, 17, 36)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1129, 21, 33)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1130, 20, 33)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1131, 10, 33)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1132, 21, 1045)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1133, 16, 1045)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1134, 17, 1045)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1135, 20, 1045)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1136, 10, 1045)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1137, 1030, 1046)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1138, 10, 1047)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1139, 22, 34)
INSERT [dbo].[Traza_X_Servicio] ([Id], [Id_Servicio], [Id_Traza]) VALUES (1140, 1025, 1048)
SET IDENTITY_INSERT [dbo].[Traza_X_Servicio] OFF
ALTER TABLE [dbo].[Estacion_X_Incidente]  WITH CHECK ADD  CONSTRAINT [FK_Estaciones_X_Incidentes__Estaciones] FOREIGN KEY([Id_Estacion])
REFERENCES [dbo].[Estacion] ([Id])
GO
ALTER TABLE [dbo].[Estacion_X_Incidente] CHECK CONSTRAINT [FK_Estaciones_X_Incidentes__Estaciones]
GO
ALTER TABLE [dbo].[Estacion_X_Incidente]  WITH CHECK ADD  CONSTRAINT [FK_Estaciones_X_Incidentes__Incidentes] FOREIGN KEY([Id_Incidente])
REFERENCES [dbo].[Incidente] ([Id])
GO
ALTER TABLE [dbo].[Estacion_X_Incidente] CHECK CONSTRAINT [FK_Estaciones_X_Incidentes__Incidentes]
GO
ALTER TABLE [dbo].[Formacion_X_Coche]  WITH CHECK ADD  CONSTRAINT [FK_Formaciones_X_Coches__Coche] FOREIGN KEY([Id_Coche])
REFERENCES [dbo].[Coche] ([Id])
GO
ALTER TABLE [dbo].[Formacion_X_Coche] CHECK CONSTRAINT [FK_Formaciones_X_Coches__Coche]
GO
ALTER TABLE [dbo].[Formacion_X_Coche]  WITH CHECK ADD  CONSTRAINT [FK_Formaciones_X_Coches__Formaciones] FOREIGN KEY([Id_Formacion])
REFERENCES [dbo].[Formacion] ([Id])
GO
ALTER TABLE [dbo].[Formacion_X_Coche] CHECK CONSTRAINT [FK_Formaciones_X_Coches__Formaciones]
GO
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Estacion] FOREIGN KEY([Id_Estacion_Mantenimiento])
REFERENCES [dbo].[Estacion] ([Id])
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Servicio_Estacion]
GO
ALTER TABLE [dbo].[Servicio_X_Formacion]  WITH CHECK ADD  CONSTRAINT [FK_Servicios_X_Formaciones__Formacion] FOREIGN KEY([Id_Formacion])
REFERENCES [dbo].[Formacion] ([Id])
GO
ALTER TABLE [dbo].[Servicio_X_Formacion] CHECK CONSTRAINT [FK_Servicios_X_Formaciones__Formacion]
GO
ALTER TABLE [dbo].[Servicio_X_Formacion]  WITH CHECK ADD  CONSTRAINT [FK_Servicios_X_Formaciones__Servicio] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[Servicio] ([Id])
GO
ALTER TABLE [dbo].[Servicio_X_Formacion] CHECK CONSTRAINT [FK_Servicios_X_Formaciones__Servicio]
GO
ALTER TABLE [dbo].[Simulacion]  WITH CHECK ADD  CONSTRAINT [FK_Simulacion_Traza] FOREIGN KEY([Id_Traza])
REFERENCES [dbo].[Traza] ([Id])
GO
ALTER TABLE [dbo].[Simulacion] CHECK CONSTRAINT [FK_Simulacion_Traza]
GO
ALTER TABLE [dbo].[Tramo]  WITH CHECK ADD  CONSTRAINT [FK_Tramo_Estacion] FOREIGN KEY([Id_Estacion_Anterior])
REFERENCES [dbo].[Estacion] ([Id])
GO
ALTER TABLE [dbo].[Tramo] CHECK CONSTRAINT [FK_Tramo_Estacion]
GO
ALTER TABLE [dbo].[Tramo]  WITH CHECK ADD  CONSTRAINT [FK_Tramo_Estacion1] FOREIGN KEY([Id_Estacion_Siguiente])
REFERENCES [dbo].[Estacion] ([Id])
GO
ALTER TABLE [dbo].[Tramo] CHECK CONSTRAINT [FK_Tramo_Estacion1]
GO
ALTER TABLE [dbo].[Tramo]  WITH CHECK ADD  CONSTRAINT [FK_Tramo_Servicio] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[Servicio] ([Id])
GO
ALTER TABLE [dbo].[Tramo] CHECK CONSTRAINT [FK_Tramo_Servicio]
GO
ALTER TABLE [dbo].[Traza_X_Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Trazas_X_Servicios__Servicio] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[Servicio] ([Id])
GO
ALTER TABLE [dbo].[Traza_X_Servicio] CHECK CONSTRAINT [FK_Trazas_X_Servicios__Servicio]
GO
ALTER TABLE [dbo].[Traza_X_Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Trazas_X_Servicios__Traza] FOREIGN KEY([Id_Traza])
REFERENCES [dbo].[Traza] ([Id])
GO
ALTER TABLE [dbo].[Traza_X_Servicio] CHECK CONSTRAINT [FK_Trazas_X_Servicios__Traza]
GO
USE [master]
GO
ALTER DATABASE [SimuRails_DB] SET  READ_WRITE 
GO
