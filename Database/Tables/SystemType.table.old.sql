/****** Object:  Table [dbo].[SystemType]    Script Date: 6/4/2016 11:06:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemType]') AND type in (N'U'))
DROP TABLE [dbo].[SystemType]
GO

/****** Object:  Table [dbo].[SystemType]    Script Date: 6/4/2016 11:06:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemType](
	[SystemTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SystemType] PRIMARY KEY CLUSTERED 
(
	[SystemTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET IDENTITY_INSERT [dbo].[SystemType] ON
GO
INSERT [dbo].[SystemType] ([SystemTypeId], [Description]) VALUES (1, N'Otpuskarche')
GO
INSERT [dbo].[SystemType] ([SystemTypeId], [Description]) VALUES (2, N'Booking')
GO
SET IDENTITY_INSERT [dbo].[SystemType] OFF
GO


