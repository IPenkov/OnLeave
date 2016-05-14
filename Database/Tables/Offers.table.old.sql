IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_UtilityBuilding]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK_Offer_UtilityBuilding]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_OfferType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK_Offer_OfferType]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OfferPhotoDetail_Offer]') AND parent_object_id = OBJECT_ID(N'[dbo].[OfferPhotoDetails]'))
ALTER TABLE [dbo].[OfferPhotoDetails] DROP CONSTRAINT [FK_OfferPhotoDetail_Offer]
GO

/****** Object:  Table [dbo].[Offers]    Script Date: 4/9/2016 8:24:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Offers]') AND type in (N'U'))
DROP TABLE [dbo].[Offers]
GO

/****** Object:  Table [dbo].[Offers]    Script Date: 4/9/2016 8:24:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Offers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Offers](
	[OfferId] [int] IDENTITY(1,1) NOT NULL,
	[KeyWords] [nvarchar](255) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Description] [nvarchar](max) NOT NULL,
	[OfferTypeId] [int] NOT NULL,
	[UtilityBuildingId] [int] NOT NULL,
	[Discount] [tinyint] NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[OfferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_OfferType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD  CONSTRAINT [FK_Offer_OfferType] FOREIGN KEY([OfferTypeId])
REFERENCES [dbo].[OfferTypes] ([OfferTypeId])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_OfferType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers] CHECK CONSTRAINT [FK_Offer_OfferType]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_UtilityBuilding]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers]  WITH CHECK ADD  CONSTRAINT [FK_Offer_UtilityBuilding] FOREIGN KEY([UtilityBuildingId])
REFERENCES [dbo].[UtilityBuildings] ([UtilityBuildingId])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Offer_UtilityBuilding]') AND parent_object_id = OBJECT_ID(N'[dbo].[Offers]'))
ALTER TABLE [dbo].[Offers] CHECK CONSTRAINT [FK_Offer_UtilityBuilding]
GO


