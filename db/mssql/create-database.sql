USE [master]
GO

IF DB_ID('Contacts') IS NOT NULL
  set noexec on               -- prevent creation when already exists

CREATE DATABASE [Contacts];
GO

USE [Contacts]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
    [Age] [int] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Contacts] ON
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age]) VALUES (1, N'Loic', N'BLAIR', 33)
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age]) VALUES (2, N'Mélanie', N'SULLIVAN', 52)
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
