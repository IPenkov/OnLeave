UPDATE [dbo].[UtilityBuildingTypes]
   SET [KeyWords] = 'Хотел'
      ,[Name] = 'Хотел'
      ,[Description] = 'Хотел'
 WHERE UtilityBuildingTypeId = 1


INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('Семеен хотел'
           ,'Семеен хотел'
           ,'Семеен хотел')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('Kъща'
           ,'Kъща'
           ,'Kъща')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('Апартамент'
           ,'Апартамент'
           ,'Апартамент')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('Бунгало'
           ,'Бунгало'
           ,'Бунгало')

GO

ALTER TABLE dbo.UtilityBuildings ADD
	Rating tinyint NULL

