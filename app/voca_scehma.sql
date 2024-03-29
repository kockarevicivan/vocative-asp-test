USE [master]
GO
/****** Object:  Database [Voca]    Script Date: 1/6/2018 2:15:33 PM ******/
CREATE DATABASE [Voca]
GO
USE [Voca]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 1/6/2018 2:15:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[ApiKey] [nvarchar](128) NOT NULL,
	[Secret] [nvarchar](128) NOT NULL,
	[ActiveUntil] [datetime] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Noun]    Script Date: 1/6/2018 2:15:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Noun](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nominative] [nvarchar](64) NOT NULL,
	[Genitive] [nvarchar](64) NOT NULL,
	[Dative] [nvarchar](64) NOT NULL,
	[Accusative] [nvarchar](64) NOT NULL,
	[Vocative] [nvarchar](64) NOT NULL,
	[Instrumental] [nvarchar](64) NOT NULL,
	[Locative] [nvarchar](64) NOT NULL,
	[IsGuaranteed] [bit] NOT NULL,
 CONSTRAINT [PK_Noun] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 1/6/2018 2:15:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[DateRegistered] [datetime] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[VerificationToken] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [Password], [DateRegistered], [IsVerified], [VerificationToken]) VALUES (1, N'kockarevic.ivan@gmail.com', N'asdf', CAST(N'2018-01-06 00:00:00.000' AS DateTime), 1, N'fgGt38hdfgU44hsdf2')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO
USE [master]
GO
ALTER DATABASE [Voca] SET  READ_WRITE 
GO
