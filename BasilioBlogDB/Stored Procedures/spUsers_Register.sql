CREATE PROCEDURE [dbo].[spUsers_Register]
    @userName NVARCHAR(16),
    @firstName NVARCHAR(50),
    @lastName NVARCHAR(50),
    @password NVARCHAR(16)
AS
begin
    set nocount on;

    INSERT INTO dbo.Users (UserName, FirstName, LastName, Password)
    VALUES (@userName, @firstName, @lastName, @password);
end