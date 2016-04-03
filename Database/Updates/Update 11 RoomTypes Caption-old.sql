BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[RoomType]
   SET [Description] = 'двойна стая'
 WHERE RoomTypeId = 1

 UPDATE [dbo].[RoomType]
   SET [Description] = 'тройна стая'
 WHERE RoomTypeId = 2

 UPDATE [dbo].[RoomType]
   SET [Description] = 'цяла къща/вила'
 WHERE RoomTypeId = 5

COMMIT TRANSACTION

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH