BEGIN TRANSACTION
ALTER TABLE dbo.UtilityBuildings ADD Description_Temp NVARCHAR(1024) NULL
GO
UPDATE dbo.UtilityBuildings SET [Description_Temp] = [Description] 
ALTER TABLE dbo.UtilityBuildings DROP COLUMN Description
EXEC sp_rename 'UtilityBuildings.Description_Temp', 'Description', 'COLUMN'
IF @@ERROR = 0
BEGIN
  COMMIT TRANSACTION
END ELSE
BEGIN 
	ROLLBACK TRANSACTION 
END
