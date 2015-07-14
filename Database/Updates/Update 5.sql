BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[UtilityBuildingFacilityType]
   SET [Name] = LOWER([Name])
 WHERE UtilityBuildingFacilityTypeId != 20

INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('баня/тоалетна')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('барбекю')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('гараж')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('голф игрище')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('гладене')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('достъп за инвалиди')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('екскурзовод')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('закрит басейн')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('изглед')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('интернет')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('кабелна тв.')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('камина')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('кафе машина')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('климатик')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('кухня')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('кухненски бокс')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('лоби бар')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('лятна градина')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('зимна градина')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('механа')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('микровълнова')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('мини бар')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('обменно бюро')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('открит басейн')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('охрана')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('отопление')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('паркинг')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('пералня')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('плащане с карти')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('ресторант')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('сателитна тв.')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('сауна')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('сешоар')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('ски гардероб')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('студио за масаж')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('телевизор')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('тенис на маса')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('тенис на корт')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('фитнес')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('хладилник')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('ютия')


INSERT INTO [dbo].[UtilityBuildingFacilityType]
           ([Name])
     VALUES
           ('хидромасажна вана')


UPDATE [dbo].[UtilityBuildingFacilityType]
   SET [Name] = 'СПА център'
 WHERE UtilityBuildingFacilityTypeId = 21


COMMIT TRANSACTION;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH


