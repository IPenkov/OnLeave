/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.UtilityBuildings ADD
	SystemTypeId int
GO

UPDATE dbo.UtilityBuildings SET SystemTypeId = 1
GO

ALTER TABLE dbo.UtilityBuildings ALTER COLUMN
	SystemTypeId int NOT NULL
GO

ALTER TABLE dbo.UtilityBuildings ADD
	ExternalUrl nvarchar(255) NULL

