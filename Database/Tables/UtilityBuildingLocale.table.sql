ALTER TABLE [dbo].[UtilityBuildingLocale] DROP CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildings]
GO

ALTER TABLE [dbo].[UtilityBuildingLocale] DROP CONSTRAINT [FK_UtilityBuildingLocale_UtilityBuildingLocale]
GO

/****** Object:  Table [dbo].[UtilityBuildingLocale]    Script Date: 7/4/2015 10:10:00 AM ******/
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


