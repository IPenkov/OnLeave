BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[UtilityBuildingFacilityType]
   SET [Name] = LOWER([Name])
 WHERE UtilityBuildingFacilityTypeId != 20

INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����/��������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���� ������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������ �� ��������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�����������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������ ������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('��������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������� ��.')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���� ������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('��������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('��������� ����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���� ���')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����� �������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����� �������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���� ���')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������� ����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������ ������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������� � �����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('��������� ��.')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('�����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('��� ��������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������ �� �����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����� �� ����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����� �� ����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('���������')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('����')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('������������ ����')


UPDATE [dbo].[UtilityBuildingFacilityType]
   SET [Name] = '��� ������'
 WHERE UtilityBuildingFacilityTypeId = 21


COMMIT TRANSACTION;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH


