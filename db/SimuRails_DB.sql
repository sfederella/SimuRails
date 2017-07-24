USE [master]
GO
/****** Object:  Database [SimuRails_DB]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Coche]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Estacion]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Estacion_X_Incidente]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Formacion]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Formacion_X_Coche]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Incidente]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Servicio]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Servicio_X_Formacion]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Simulacion]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Tramo]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
	[VelocidadPromedio] [int] NOT NULL,
	[TiempoViaje] [int] NOT NULL,
	[EstSigEsParada] [bit] NOT NULL,
 CONSTRAINT [PK_Relaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Traza]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
/****** Object:  Table [dbo].[Traza_X_Servicio]    Script Date: 24/7/2017 1:58:26 a. m. ******/
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
