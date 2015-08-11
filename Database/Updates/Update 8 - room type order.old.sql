ALTER TABLE dbo.RoomType ADD
	[Order] smallint NULL
GO

UPDATE [dbo].[RoomType]
   SET [Order] = 1
 WHERE RoomTypeId = 1

 UPDATE [dbo].[RoomType]
   SET [Order] = 2
 WHERE RoomTypeId = 2

 UPDATE [dbo].[RoomType]
   SET [Order] = 3
 WHERE RoomTypeId = 3

 UPDATE [dbo].[RoomType]
   SET [Order] = 4
 WHERE RoomTypeId = 4

 UPDATE [dbo].[RoomType]
   SET [Order] = 5
 WHERE RoomTypeId = 5

 GO

 ALTER TABLE dbo.RoomType ALTER COLUMN
	[Order] smallint NOT NULL
GO
