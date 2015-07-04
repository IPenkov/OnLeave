SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 4/27/2014 12:55:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertUser]
GO

-- =============================================
-- Author:		Ivan Penkov
-- Create date: 27.04.2014
-- Description:	Insert User
-- =============================================
CREATE PROCEDURE dbo.InsertUser
	-- Add the parameters for the stored procedure here
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Telephone NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

	INSERT INTO [dbo].[User]
			   ([KeyWords]
			   ,[FirstName]
			   ,[LastName]
			   ,[Email]
			   ,[Telephone]
			   ,[Password])
		 VALUES
			   (@FirstName + ' ' + @LastName
			   ,@FirstName
			   ,@LastName
			   ,@Email
			   ,@Telephone
			   ,HashBytes('SHA2_512', @Password))
			  

    -- Return user id
	RETURN SCOPE_IDENTITY()
END
GO
