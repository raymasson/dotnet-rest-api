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
	[Gender] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Contacts] ON
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (1, N'Loic', N'BLAIR', 33, N'male')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (2, N'Mélanie', N'SULLIVAN', 52, N'female')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (3, N'Tom', N'BOYER', 2, N'male')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (4, N'Stéphanie', N'DURAND', 13, N'female')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (5, N'Raymond', N'THOMSON', 92, N'male')
GO
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Age], [Gender]) VALUES (6, N'Thomas', N'THOMSON', 93, N'male')
GO
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[ZipCode] [int] NOT NULL,
	[City] [varchar](50) NOT NULL,
    [ContactId] [int] NOT NULL,
FOREIGN KEY (ContactId) REFERENCES Contacts(Id)
) ON [PRIMARY]
GO

INSERT [dbo].[Addresses] ([ZipCode], [City], [ContactId]) VALUES (06130, N'GRASSE', 1)
GO
INSERT [dbo].[Addresses] ([ZipCode], [City], [ContactId]) VALUES (44000, N'NANTES', 4)
GO
