USE [master]
GO
/****** Object:  Database [Veiculos]    Script Date: 18/10/2024 15:38:48 ******/
CREATE DATABASE [Veiculos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Veiculos', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Veiculos.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Veiculos_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Veiculos_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Veiculos] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Veiculos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Veiculos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Veiculos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Veiculos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Veiculos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Veiculos] SET ARITHABORT OFF 
GO
ALTER DATABASE [Veiculos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Veiculos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Veiculos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Veiculos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Veiculos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Veiculos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Veiculos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Veiculos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Veiculos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Veiculos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Veiculos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Veiculos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Veiculos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Veiculos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Veiculos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Veiculos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Veiculos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Veiculos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Veiculos] SET  MULTI_USER 
GO
ALTER DATABASE [Veiculos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Veiculos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Veiculos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Veiculos] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Veiculos] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Veiculos]
GO
/****** Object:  User [sa]    Script Date: 18/10/2024 15:38:48 ******/
CREATE USER [sa] FOR LOGIN [AUTORIDADE NT\SISTEMA] WITH DEFAULT_SCHEMA=[db_accessadmin]
GO
/****** Object:  User [admin]    Script Date: 18/10/2024 15:38:48 ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Combustivel]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combustivel](
	[CombustivelId] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CombustivelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Combustivel_Nome] UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cor]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cor](
	[CorId] [int] IDENTITY(1,1) NOT NULL,
	[NomeCor] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[NomeUsuario] [nvarchar](50) NOT NULL,
	[Senha] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime] NULL DEFAULT (getdate()),
	[TipoUsuario] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NomeUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Veiculos]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Veiculos](
	[VeiculosId] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [char](7) NOT NULL,
	[Renavam] [int] NOT NULL,
	[Chassi] [nvarchar](17) NOT NULL,
	[Motor] [nvarchar](20) NOT NULL,
	[Marca] [text] NOT NULL,
	[Modelo] [text] NOT NULL,
	[Combustivel] [nvarchar](50) NOT NULL,
	[Cor] [nvarchar](50) NOT NULL,
	[Ano] [int] NOT NULL,
	[Situacao] [varchar](10) NULL,
 CONSTRAINT [PK_Veiculos] PRIMARY KEY CLUSTERED 
(
	[VeiculosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[AtualizarVeiculo]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	Create Procedure [dbo].[AtualizarVeiculo]
	(
	
		@VeiculosId int,
		@Placa char(7),
		@Renavam int,
		@Chassi nvarchar(17),
		@Motor nvarchar(20),
		@Marca text,
		@Modelo text,
		@Combustivel nvarchar(50),
		@Cor nvarchar(50),
		@Ano int
	
	
	)

	as 

	begin

	Update Veiculos set Placa = @Placa,
	Renavam = @Renavam, Chassi = @Chassi,
	Motor = @Motor, Marca = @Marca, Modelo = @Modelo,
	Combustivel = @Combustivel, Cor = @Cor, Ano = @Ano
	where VeiculosId = @VeiculosId

	end
GO
/****** Object:  StoredProcedure [dbo].[ExcluirVeiculo]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ExcluirVeiculo]
(
	@VeiculosId int

)

as

Begin
	Delete from Veiculos where VeiculosId = @VeiculosId
end
GO
/****** Object:  StoredProcedure [dbo].[IncluirVeiculo]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE Procedure [dbo].[IncluirVeiculo]
	(
	
		@Placa char(7),
		@Renavam int,
		@Chassi nvarchar(17),
		@Motor nvarchar(20),
		@Marca text,
		@Modelo text,
		@Combustivel nvarchar(50),
		@Cor nvarchar(50),
		@Ano int,
		@Situacao varchar(7)
	
	
	
	)

	as 

	begin

		Insert into Veiculos values (@Placa, @Renavam, @Chassi, @Motor, @Marca, @Modelo, @Combustivel, @Cor, @Ano, @Situacao)

	end
GO
/****** Object:  StoredProcedure [dbo].[ObterVeiculos]    Script Date: 18/10/2024 15:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ObterVeiculos]

as

begin

	Select VeiculosId, Placa, Renavam, Chassi, Motor, Marca, Modelo, Combustivel, Cor, Ano, Situacao from Veiculos
end
GO
USE [master]
GO
ALTER DATABASE [Veiculos] SET  READ_WRITE 
GO
