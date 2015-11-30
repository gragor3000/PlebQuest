--Donne les stats de la race donnée
CREATE PROCEDURE sp_RaceStats(@RaceID int)
AS
BEGIN
	SELECT * FROM Race
	WHERE Race.RaceID = @RaceID
END