CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [uname] VARCHAR(50) NOT NULL, 
    [pwd] VARCHAR(50) NOT NULL, 
    [user_role] VARCHAR(50) NOT NULL
)
