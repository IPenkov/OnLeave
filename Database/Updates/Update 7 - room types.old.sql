BEGIN TRY
	BEGIN TRANSACTION

UPDATE [dbo].[RoomType]
   SET [Description] = '���������� ����'
 WHERE RoomTypeId = 1

 UPDATE [dbo].[RoomType]
   SET [Description] = '���� ����'
 WHERE RoomTypeId = 2

 UPDATE [dbo].[RoomType]
   SET [Description] = '������'
 WHERE RoomTypeId = 3

INSERT INTO [dbo].[RoomType]
           ([DBC_KEYWORDS]
           ,[Description])
     VALUES
           ('����������'
           ,'����������')

INSERT INTO [dbo].[RoomType]
           ([DBC_KEYWORDS]
           ,[Description])
     VALUES
           ('���� ����������'
           ,'���� ����������')

COMMIT TRANSACTION

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH


