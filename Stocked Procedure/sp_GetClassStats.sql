--donne les Stats de la class donne
CREATE PROCEDURE sp_GetClassStats(@ClassID int)
AS
BEGIN
	SELECT * From Class
	WHERE Class.ClassID = @ClassID
END



