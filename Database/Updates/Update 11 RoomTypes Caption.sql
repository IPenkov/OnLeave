BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[RoomType]
   SET [Description] = '������ ����'
 WHERE RoomTypeId = 1

 UPDATE [dbo].[RoomType]
   SET [Description] = '������ ����'
 WHERE RoomTypeId = 2

 UPDATE [dbo].[RoomType]
   SET [Description] = '���� ����/����'
 WHERE RoomTypeId = 5

COMMIT TRANSACTION

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH