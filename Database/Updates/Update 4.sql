BEGIN TRY
	BEGIN TRANSACTION

	INSERT INTO [dbo].[Locale] ([Name]) VALUES ('BG')	
	INSERT INTO [dbo].[Locale] ([Name]) VALUES ('EN')	

	INSERT INTO [dbo].[UtilityBuildingLocale]
			   ([UtilityBuildingId]
			   ,[LocaleId]
			   ,[Description]
			   ,[Name]
			   ,[ContactPerson]
			   ,[Address])     
		SELECT 
		B.UtilityBuildingId
		,L.LocaleId
		,B.Description
		,B.Name
		,B.ContactPerson
		,B.[Address]
		FROM dbo.UtilityBuildings B
		INNER JOIN dbo.Locale L ON L.Name = 'BG'
	

	ALTER TABLE dbo.UtilityBuildings
	DROP COLUMN Name
	ALTER TABLE dbo.UtilityBuildings
	DROP COLUMN [Description]
	ALTER TABLE dbo.UtilityBuildings
	DROP COLUMN ContactPerson	
	ALTER TABLE dbo.UtilityBuildings
	DROP COLUMN [Address]

	COMMIT TRANSACTION;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH



