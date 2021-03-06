USE [master]
GO
/****** Object:  Database [Giallo]    Script Date: 24/08/2015 16:12:32 ******/
CREATE DATABASE [Giallo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Giallo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Giallo.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Giallo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Giallo_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Giallo] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Giallo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Giallo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Giallo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Giallo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Giallo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Giallo] SET ARITHABORT OFF 
GO
ALTER DATABASE [Giallo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Giallo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Giallo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Giallo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Giallo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Giallo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Giallo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Giallo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Giallo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Giallo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Giallo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Giallo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Giallo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Giallo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Giallo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Giallo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Giallo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Giallo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Giallo] SET  MULTI_USER 
GO
ALTER DATABASE [Giallo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Giallo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Giallo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Giallo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Giallo] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Giallo]
GO
/****** Object:  Table [dbo].[administradores]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[administradores](
	[idAdministrador] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NULL,
	[contraseña] [varchar](50) NULL,
 CONSTRAINT [PK_administradores] PRIMARY KEY CLUSTERED 
(
	[idAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[articulos]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[articulos](
	[codigoArticulo] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NULL,
 CONSTRAINT [PK_articulos] PRIMARY KEY CLUSTERED 
(
	[codigoArticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[colores]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[colores](
	[codigoColor] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_colores] PRIMARY KEY CLUSTERED 
(
	[codigoColor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[entregas]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[entregas](
	[codigoEntrega] [int] IDENTITY(1,1) NOT NULL,
	[fechaInicio] [datetime] NULL,
	[fechaFin] [datetime] NULL,
	[codigoTandaItems] [int] NULL,
 CONSTRAINT [PK_entregas] PRIMARY KEY CLUSTERED 
(
	[codigoEntrega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[talleres]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[talleres](
	[codigoTaller] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[responsable] [varchar](100) NULL,
	[contacto] [varchar](100) NULL,
	[codigoTipoTaller] [int] NULL,
 CONSTRAINT [PK_talleres] PRIMARY KEY CLUSTERED 
(
	[codigoTaller] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[talles]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[talles](
	[codigoTalle] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[codigoTipo] [int] NOT NULL,
 CONSTRAINT [PK_talles] PRIMARY KEY CLUSTERED 
(
	[codigoTalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tandaItems]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tandaItems](
	[codigoTandaItems] [int] IDENTITY(1,1) NOT NULL,
	[codigoTanda] [int] NULL,
	[codigoTalle] [int] NULL,
	[codigoColor] [int] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_tandaItems] PRIMARY KEY CLUSTERED 
(
	[codigoTandaItems] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tandas]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tandas](
	[codigoTanda] [int] IDENTITY(1,1) NOT NULL,
	[comentario] [varchar](100) NULL,
	[codigoArticulo] [int] NULL,
	[fechaInicio] [datetime] NULL,
	[fechaFin] [datetime] NULL,
	[estampado] [varchar](max) NULL,
 CONSTRAINT [PK_tandas] PRIMARY KEY CLUSTERED 
(
	[codigoTanda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipos]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipos](
	[codigoTipo] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_tipos] PRIMARY KEY CLUSTERED 
(
	[codigoTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipoTalleres]    Script Date: 24/08/2015 16:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoTalleres](
	[codigoTipoTaller] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_tipoTalleres] PRIMARY KEY CLUSTERED 
(
	[codigoTipoTaller] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[entregas]  WITH CHECK ADD  CONSTRAINT [FK_entregas_tandaItems] FOREIGN KEY([codigoTandaItems])
REFERENCES [dbo].[tandaItems] ([codigoTandaItems])
GO
ALTER TABLE [dbo].[entregas] CHECK CONSTRAINT [FK_entregas_tandaItems]
GO
ALTER TABLE [dbo].[talleres]  WITH CHECK ADD  CONSTRAINT [FK_talleres_tipoTalleres] FOREIGN KEY([codigoTipoTaller])
REFERENCES [dbo].[tipoTalleres] ([codigoTipoTaller])
GO
ALTER TABLE [dbo].[talleres] CHECK CONSTRAINT [FK_talleres_tipoTalleres]
GO
ALTER TABLE [dbo].[talles]  WITH CHECK ADD  CONSTRAINT [FK_talles_tipos] FOREIGN KEY([codigoTipo])
REFERENCES [dbo].[tipos] ([codigoTipo])
GO
ALTER TABLE [dbo].[talles] CHECK CONSTRAINT [FK_talles_tipos]
GO
ALTER TABLE [dbo].[tandaItems]  WITH CHECK ADD  CONSTRAINT [FK_tandaItems_colores] FOREIGN KEY([codigoColor])
REFERENCES [dbo].[colores] ([codigoColor])
GO
ALTER TABLE [dbo].[tandaItems] CHECK CONSTRAINT [FK_tandaItems_colores]
GO
ALTER TABLE [dbo].[tandaItems]  WITH CHECK ADD  CONSTRAINT [FK_tandaItems_talles] FOREIGN KEY([codigoTalle])
REFERENCES [dbo].[talles] ([codigoTalle])
GO
ALTER TABLE [dbo].[tandaItems] CHECK CONSTRAINT [FK_tandaItems_talles]
GO
ALTER TABLE [dbo].[tandaItems]  WITH CHECK ADD  CONSTRAINT [FK_tandaItems_tandas] FOREIGN KEY([codigoTanda])
REFERENCES [dbo].[tandas] ([codigoTanda])
GO
ALTER TABLE [dbo].[tandaItems] CHECK CONSTRAINT [FK_tandaItems_tandas]
GO
ALTER TABLE [dbo].[tandas]  WITH CHECK ADD  CONSTRAINT [FK_tandas_articulos] FOREIGN KEY([codigoArticulo])
REFERENCES [dbo].[articulos] ([codigoArticulo])
GO
ALTER TABLE [dbo].[tandas] CHECK CONSTRAINT [FK_tandas_articulos]
GO
USE [master]
GO
ALTER DATABASE [Giallo] SET  READ_WRITE 
GO
