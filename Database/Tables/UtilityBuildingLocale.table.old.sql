IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UtilityBuildingLocale_UtilityBuildings]') AND parent_object_id = OBJECT_ID(N'[dbo].[UtilityBuildingLocale]'))
ALTER TABLE [dbo].[UtilityBuildingLocale] DROP CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildings]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UtilityBuildingLocale_UtilityBuildingLocale]') AND parent_object_id = OBJECT_ID(N'[dbo].[UtilityBuildingLocale]'))
ALTER TABLE [dbo].[UtilityBuildingLocale] DROP CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildingLocale]
GO

/****** Object:  Table [dbo].[UtilityBuildingLocale]    Script Date: 7/8/2015 10:14:27 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UtilityBuildingLocale]') AND type in (N'U'))
DROP TABLE [dbo].[UtilityBuildingLocale]
GO

/****** Object:  Table [dbo].[UtilityBuildingLocale]    Script Date: 7/4/2015 10:10:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UtilityBuildingLocale](
	[UtilityBuildingLocaleId] [int] IDENTITY(1,1) NOT NULL,
	[UtilityBuildingId] [int] NOT NULL,
	[LocaleId] [int] NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ContactPerson] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_UtilityBuildingLocale] PRIMARY KEY CLUSTERED 
(
	[UtilityBuildingLocaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UtilityBuildingLocale]  WITH CHECK ADD  CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildingLocale] FOREIGN KEY([LocaleId])
REFERENCES [dbo].[Locale] ([LocaleId])
GO

ALTER TABLE [dbo].[UtilityBuildingLocale] CHECK CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildingLocale]
GO

ALTER TABLE [dbo].[UtilityBuildingLocale]  WITH CHECK ADD  CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildings] FOREIGN KEY([UtilityBuildingId])
REFERENCES [dbo].[UtilityBuildings] ([UtilityBuildingId])
GO

ALTER TABLE [dbo].[UtilityBuildingLocale] CHECK CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildings]
GO


