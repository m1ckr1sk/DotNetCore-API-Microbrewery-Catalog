USE master
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='MicrobreweryDatabase')
DROP DATABASE MicrobreweryDatabase
GO

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE MicrobreweryDatabase
  ON PRIMARY (NAME = N''MicrobreweryDatabase'', FILENAME = N''' + @device_directory + N'MicrobreweryDatabase.mdf'')
  LOG ON (NAME = N''MicrobreweryDatabase_log'',  FILENAME = N''' + @device_directory + N'MicrobreweryDatabase.ldf'')')
GO

USE MicrobreweryDatabase
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Beer]') AND type in (N'U'))
DROP TABLE [dbo].[Beer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Microbrewery]') AND type in (N'U'))
DROP TABLE [dbo].[Microbrewery]
GO

USE [MicrobreweryDatabase]
GO

/****** Object:  Table [dbo].[Microbrewery]    Script Date: 11/14/2017 10:32:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Microbrewery](
	[Id] [uniqueidentifier] NOT NULL,
	[FoundedOn] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NumberOfStills] [int] NOT NULL,
	[Owner] [nvarchar](max) NULL,
 CONSTRAINT [PK_Microbrewery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [MicrobreweryDatabase]
GO

/****** Object:  Table [dbo].[Beer]    Script Date: 11/14/2017 10:31:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Beer](
	[Id] [uniqueidentifier] NOT NULL,
	[Abv] [decimal](18, 2) NOT NULL,
	[IsGlutenFree] [bit] NOT NULL,
	[MicrobreweryId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Beer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Beer]  WITH CHECK ADD  CONSTRAINT [FK_Beer_Microbrewery_MicrobreweryId] FOREIGN KEY([MicrobreweryId])
REFERENCES [dbo].[Microbrewery] ([Id])
GO

ALTER TABLE [dbo].[Beer] CHECK CONSTRAINT [FK_Beer_Microbrewery_MicrobreweryId]
GO


