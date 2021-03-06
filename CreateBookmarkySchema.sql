USE [redxBookmarky]
GO
/****** Object:  Table [redxadmin].[Bookmark]    Script Date: 10/27/2015 4:42:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [redxadmin].[Bookmark](
	[BookmarkID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[Comment] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Bookmark] PRIMARY KEY CLUSTERED 
(
	[BookmarkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [redxadmin].[UserAuthToken]    Script Date: 10/27/2015 4:42:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [redxadmin].[UserAuthToken](
	[UserAuthTokenID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[AuthToken] [nvarchar](50) NOT NULL,
	[ExpireDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_UserAuthToken] PRIMARY KEY CLUSTERED 
(
	[UserAuthTokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [redxadmin].[Users]    Script Date: 10/27/2015 4:42:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [redxadmin].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL CONSTRAINT [DF_Users_IsEmailVerified]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
