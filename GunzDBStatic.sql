/*
 * "Team Zebra - Gunz Emulator" - An open-source server emulator
 * for the free, online third-person-shooter "Gunz: The Duel".
 * 
 * This project is in no way affiliated with MAIET Entertainment, Inc., 
 * ijji, NHN Inc., LevelUp! Games, or any previous or future "Gunz: The Duel"
 * publishers. All trademarks, copyrights, etc. belong to their respective 
 * owners. This project contains no code from any of the afforementioned 
 * companies.
 * 
 * Copyright 2009 Team Zebra
 * Contact at <ZebraForceFive@gmail.com>
 * 
 * This file is part of "Team Zebra - Gunz Emulator".
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

USE [master]
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GunzDBStatic')
BEGIN
CREATE DATABASE [GunzDBStatic] ON  PRIMARY 
( NAME = N'GunzDBStatic', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\GunzDBStatic.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GunzDBStatic_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\GunzDBStatic_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
EXEC dbo.sp_dbcmptlevel @dbname=N'GunzDBStatic', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GunzDBStatic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GunzDBStatic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GunzDBStatic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GunzDBStatic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GunzDBStatic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GunzDBStatic] SET ARITHABORT OFF 
GO
ALTER DATABASE [GunzDBStatic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GunzDBStatic] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GunzDBStatic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GunzDBStatic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GunzDBStatic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GunzDBStatic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GunzDBStatic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GunzDBStatic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GunzDBStatic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GunzDBStatic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GunzDBStatic] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GunzDBStatic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GunzDBStatic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GunzDBStatic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GunzDBStatic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GunzDBStatic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GunzDBStatic] SET  READ_WRITE 
GO
ALTER DATABASE [GunzDBStatic] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GunzDBStatic] SET  MULTI_USER 
GO
ALTER DATABASE [GunzDBStatic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GunzDBStatic] SET DB_CHAINING OFF 
USE [GunzDBStatic]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Items]') AND type in (N'U'))
BEGIN
CREATE TABLE [Items](
	[ItemID] [int] NULL,
	[Price] [int] NULL,
	[Level] [tinyint] NULL,
	[Weight] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Channels]') AND type in (N'U'))
BEGIN
CREATE TABLE [Channels](
	[UID] [int] IDENTITY(0,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[MaxPlayers] [int] NOT NULL,
	[Rule] [varchar](24) NOT NULL,
	[MinLevel] [tinyint] NULL CONSTRAINT [DF_Channels_MinLevel]  DEFAULT ((0)),
	[MaxLevel] [tinyint] NULL CONSTRAINT [DF_Channels_MaxLevel]  DEFAULT ((255)),
 CONSTRAINT [PK_Channels] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Shop]') AND type in (N'U'))
BEGIN
CREATE TABLE [Shop](
	[ItemID] [int] NOT NULL
) ON [PRIMARY]
END
