UPDATE [dbo].[UtilityBuildingTypes]
   SET [KeyWords] = '�����'
      ,[Name] = '�����'
      ,[Description] = '�����'
 WHERE UtilityBuildingTypeId = 1


INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('������ �����'
           ,'������ �����'
           ,'������ �����')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('K���'
           ,'K���'
           ,'K���')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('����������'
           ,'����������'
           ,'����������')

INSERT INTO [dbo].[UtilityBuildingTypes]
           ([KeyWords]
           ,[Name]
           ,[Description])
     VALUES
           ('�������'
           ,'�������'
           ,'�������')

GO

ALTER TABLE dbo.UtilityBuildings ADD
	Rating tinyint NULL

