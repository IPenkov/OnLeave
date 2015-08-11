BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[RoomType]
   SET [Description] = 'стандартна стая'
 WHERE RoomTypeId = 1

 UPDATE [dbo].[RoomType]
   SET [Description] = 'лукс стая'
 WHERE RoomTypeId = 2

 UPDATE [dbo].[RoomType]
   SET [Description] = 'студио'
 WHERE RoomTypeId = 3

INSERT INTO [dbo].[RoomType]
           ([DBC_KEYWORDS]
           ,[Description])
     VALUES
           ('апартамент'
           ,'апартамент')

INSERT INTO [dbo].[RoomType]
           ([DBC_KEYWORDS]
           ,[Description])
     VALUES
           ('лукс апартамент'
           ,'лукс апартамент')

COMMIT TRANSACTION

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH


