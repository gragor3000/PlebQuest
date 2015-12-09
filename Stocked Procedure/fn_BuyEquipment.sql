--achete et retourne une nouvelle piece d'équipement
CREATE FUNCTION fn_BuyEquipment(@CharID int)
RETURNS @Table TABLE(EquipID int)
AS
BEGIN
	DECLARE @EquipID int--id de l'équipement qu'il va acheter
	DECLARE @CharGold int--or du personnage
	SELECT @CharGold = Characters.CharactersGold FROM Characters WHERE CharactersID = @CharID
	SELECT TOP 10 percent @EquipID = Equipment.EquipID FROM Equipment WHERE EquipID NOT IN(SELECT CharactersEquipment.CharactersEquipmentEquipmentID 
	FROM CharactersEquipment INNER JOIN Equipment ON CharactersEquipmentEquipmentID = EquipID 
	WHERE CharactersEquipmentCharactersID = @CharID) AND EquipValue <= @CharGold
	ORDER BY EquipValue DESC
	INSERT INTO @Table(EquipID) VALUES(@EquipID)

	RETURN 
END



