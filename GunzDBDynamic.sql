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
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GunzDBDynamic')
BEGIN
CREATE DATABASE [GunzDBDynamic] ON  PRIMARY 
( NAME = N'GunzDBDynamic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\GunzDBDynamic.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GunzEmulator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\GunzDBDynamic_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
EXEC dbo.sp_dbcmptlevel @dbname=N'GunzDBDynamic', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GunzDBDynamic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GunzDBDynamic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET ARITHABORT OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GunzDBDynamic] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GunzDBDynamic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GunzDBDynamic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GunzDBDynamic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GunzDBDynamic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GunzDBDynamic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GunzDBDynamic] SET  READ_WRITE 
GO
ALTER DATABASE [GunzDBDynamic] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GunzDBDynamic] SET  MULTI_USER 
GO
ALTER DATABASE [GunzDBDynamic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GunzDBDynamic] SET DB_CHAINING OFF 
USE [GunzDBDynamic]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ServerProperties]') AND type in (N'U'))
BEGIN
CREATE TABLE [ServerProperties](
	[ServerID] [tinyint] NOT NULL,
	[ServerName] [varchar](64) NULL,
	[CurrPlayers] [smallint] NULL,
	[MaxPlayers] [smallint] NULL,
	[IP] [varchar](15) NULL,
	[Port] [smallint] NULL,
	[Open] [tinyint] NULL,
	[Type] [tinyint] NULL,
 CONSTRAINT [ServerStatus_PK] PRIMARY KEY CLUSTERED 
(
	[ServerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Accounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [Accounts](
	[AID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](24) NULL,
	[Password] [varchar](24) NULL,
	[UGradeID] [tinyint] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Inventories]') AND type in (N'U'))
BEGIN
CREATE TABLE [Inventories](
	[CIID] [int] IDENTITY(1,1) NOT NULL,
	[CID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
 CONSTRAINT [PK_Inventories] PRIMARY KEY CLUSTERED 
(
	[CIID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Clans]') AND type in (N'U'))
BEGIN
CREATE TABLE [Clans](
	[CLID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](24) NULL,
	[Points] [int] NOT NULL CONSTRAINT [DF_Clan_Point]  DEFAULT ((1000)),
	[MasterCID] [int] NULL,
	[Wins] [int] NOT NULL CONSTRAINT [DF__Clan__Wins__76EBA2E9]  DEFAULT ((0)),
	[Losses] [int] NOT NULL CONSTRAINT [DF__Clan__Losses__6774552F]  DEFAULT ((0)),
	[Draws] [int] NOT NULL CONSTRAINT [DF__Clan__Draws__68687968]  DEFAULT ((0)),
	[EmblemURL] [varchar](512) NULL,
	[EmblemChecksum] [int] NOT NULL CONSTRAINT [DF__Clan__EmblemChec__004002F9]  DEFAULT ((0))
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Characters]') AND type in (N'U'))
BEGIN
CREATE TABLE [Characters](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[AID] [int] NOT NULL,
	[CLID] [int] NULL,
	[CharIndex] [tinyint] NOT NULL,
	[Name] [varchar](24) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[Sex] [tinyint] NOT NULL,
	[Hair] [tinyint] NULL,
	[Face] [tinyint] NULL,
	[XP] [int] NOT NULL,
	[BP] [int] NOT NULL,
	[HP] [smallint] NULL,
	[AP] [smallint] NULL,
	[GameCount] [int] NULL,
	[KillCount] [int] NULL,
	[DeathCount] [int] NULL,
	[HeadItemID] [int] NULL,
	[ChestItemID] [int] NULL,
	[HandsItemID] [int] NULL,
	[LegsItemID] [int] NULL,
	[FeetItemID] [int] NULL,
	[Ring1ItemID] [int] NULL,
	[Ring2ItemID] [int] NULL,
	[MeleeItemID] [int] NULL,
	[PrimaryItemID] [int] NULL,
	[SecondaryItemID] [int] NULL,
	[Custom1ItemID] [int] NULL,
	[Custom2ItemID] [int] NULL,
	[ClanGrade] [int] NULL,
	[HeadCIID] [int] NULL,
	[ChestCIID] [int] NULL,
	[HandsCIID] [int] NULL,
	[LegsCIID] [int] NULL,
	[FeetCIID] [int] NULL,
	[Ring1CIID] [int] NULL,
	[Ring2CIID] [int] NULL,
	[MeleeCIID] [int] NULL,
	[PrimaryCIID] [int] NULL,
	[SecondaryCIID] [int] NULL,
	[Custom1CIID] [int] NULL,
	[Custom2CIID] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreateCharacter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [CreateCharacter]
	@AID		int,
	@CharIndex	smallint,
	@Name		varchar(24),
	@Sex		tinyint,
	@Hair		int,  
	@Face		int,
	@Costume	int
AS
SET NOCOUNT ON
BEGIN TRAN
IF EXISTS (SELECT CID FROM Characters where (AID = @AID AND CharIndex = 
			@CharIndex) OR (Name = @Name))
BEGIN	
	ROLLBACK TRAN
	return(-1)
END

DECLARE @CharIdent 		int
DECLARE @ChestCIID		int
DECLARE @LegsCIID		int
DECLARE @MeleeCIID		int
DECLARE @PrimaryCIID	int
DECLARE @SecondaryCIID  int
DECLARE @Custom1CIID	int
DECLARE @Custom2CIID	int

DECLARE @ChestItemID		int
DECLARE @LegsItemID			int
DECLARE @MeleeItemID		int
DECLARE @PrimaryItemID		int
DECLARE @SecondaryItemID	int
DECLARE @Custom1ItemID		int
DECLARE @Custom2ItemID		int

SET @SecondaryCIID = NULL
SET @SecondaryItemID = NULL

SET @Custom1CIID = NULL
SET @Custom1ItemID = NULL

SET @Custom2CIID = NULL
SET @Custom2ItemID = NULL

INSERT INTO Characters (AID, Name, CharIndex, [Level], Sex, Hair, Face, XP, BP, 
         		           GameCount, KillCount, DeathCount)
Values (@AID, @Name, @CharIndex, 255, @Sex, @Hair, @Face, 0, 999999999, 0, 0, 0)

IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
END

SET @CharIdent = @@IDENTITY

  /* Melee */
  SET @MeleeItemID = 
    CASE @Costume
    WHEN 0 THEN 1
    WHEN 1 THEN 2
    WHEN 2 THEN 1
    WHEN 3 THEN 2
    WHEN 4 THEN 2
    WHEN 5 THEN 1
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @MeleeItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END

  SET @MeleeCIID = @@IDENTITY

  /* Primary */
  SET @PrimaryItemID = 
    CASE @Costume
    WHEN 0 THEN 5001
    WHEN 1 THEN 5002
    WHEN 2 THEN 4005
    WHEN 3 THEN 4001
    WHEN 4 THEN 4002
    WHEN 5 THEN 4006
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @PrimaryItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END

  SET @PrimaryCIID = @@IDENTITY

  /* Secondary */
IF @Costume = 0 OR @Costume = 2 BEGIN
  SET @SecondaryItemID =
    CASE @Costume
    WHEN 0 THEN 4001
    WHEN 1 THEN 0
    WHEN 2 THEN 5001
    WHEN 3 THEN 4006
    WHEN 4 THEN 0
    WHEN 5 THEN 4006
    END

  IF @SecondaryItemID <> 0 BEGIN
    INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @SecondaryItemID)
    IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
    END

    SET @SecondaryCIID = @@IDENTITY
  END
END
  SET @Custom1ItemID = 
    CASE @Costume
    WHEN 0 THEN 30301
    WHEN 1 THEN 30301
    WHEN 2 THEN 30401
    WHEN 3 THEN 30401
    WHEN 4 THEN 30401
    WHEN 5 THEN 30101
    END

  /* Custom1 */
  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @Custom1ItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END

  SET @Custom1CIID = @@IDENTITY

  /* Custom2 */
IF @Costume = 4 OR @Costume = 5
BEGIN
  SET @Custom2ItemID =
    CASE @Costume
    WHEN 0 THEN 0
    WHEN 1 THEN 0
    WHEN 2 THEN 0
    WHEN 3 THEN 0
    WHEN 4 THEN 30001
    WHEN 5 THEN 30001
    END

  IF @Custom2ItemID <> 0
  BEGIN
    INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @Custom2ItemID)
    IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
    END

    SET @Custom2CIID = @@IDENTITY
  END
END

IF @Sex = 0
BEGIN

  /* Chest */
  SET @ChestItemID =
    CASE @Costume
    WHEN 0 THEN 21001
    WHEN 1 THEN 21001
    WHEN 2 THEN 21001
    WHEN 3 THEN 21001
    WHEN 4 THEN 21001
    WHEN 5 THEN 21001
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @ChestItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END

  SET @ChestCIID = @@IDENTITY

  /* Legs */
  SET @LegsItemID =
    CASE @Costume
    WHEN 0 THEN 23001
    WHEN 1 THEN 23001
    WHEN 2 THEN 23001
    WHEN 3 THEN 23001
    WHEN 4 THEN 23001
    WHEN 5 THEN 23001
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @LegsItemID)
  IF 0 <> @@ERROR BEGIN 
	ROLLBACK TRAN
	RETURN (-1)
  END

  SET @LegsCIID = @@IDENTITY

END
ELSE
BEGIN

  /* Chest */
  SET @ChestItemID =
    CASE @Costume
    WHEN 0 THEN 21501
    WHEN 1 THEN 21501
    WHEN 2 THEN 21501
    WHEN 3 THEN 21501
    WHEN 4 THEN 21501
    WHEN 5 THEN 21501
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @ChestItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END
  SET @ChestCIID = @@IDENTITY

  /* Legs */
  SET @LegsItemID =
    CASE @Costume
    WHEN 0 THEN 23501
    WHEN 1 THEN 23501
    WHEN 2 THEN 23501
    WHEN 3 THEN 23501
    WHEN 4 THEN 23501
    WHEN 5 THEN 23501
    END

  INSERT INTO Inventories (CID, ItemID) Values (@CharIdent, @LegsItemID)
  IF 0 <> @@ERROR BEGIN
	ROLLBACK TRAN
	RETURN (-1)
  END
  SET @LegsCIID = @@IDENTITY

END  

UPDATE Characters
SET ChestCIID = @ChestCIID, LegsCIID = @LegsCIID, MeleeCIID = @MeleeCIID,
    PrimaryCIID = @PrimaryCIID, SecondaryCIID = @SecondaryCIID, Custom1CIID = @Custom1CIID,
    Custom2CIID = @Custom2CIID, ChestItemID = @ChestItemID, LegsItemID = @LegsItemID, 
	MeleeItemID = @MeleeItemID, PrimaryItemID = @PrimaryItemID, SecondaryItemID = @SecondaryItemID, 
	Custom1ItemID = @Custom1ItemID, Custom2ItemID = @Custom2ItemID
WHERE CID = @CharIdent

IF 0 = @@ROWCOUNT BEGIN
	ROLLBACK TRAN
	RETURN (-1)
END

COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DeleteCharacter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [DeleteCharacter]
	@AID		int,
	@CharIndex	smallint,
	@Name		varchar(24)
AS
SET NOCOUNT ON
DECLARE @CID		int

SELECT @CID = CID FROM Characters WHERE AID = @AID AND CharIndex = @CharIndex AND Name = @Name
IF (@CID IS NULL)
BEGIN
	return (-1)
END

DELETE FROM Characters WHERE CID = @CID
DELETE FROM Inventories WHERE CID = @CID
UPDATE Characters SET CharIndex = CharIndex - 1 WHERE AID = @AID AND CharIndex > @CharIndex

SELECT 1 AS Ret
' 
END
