--donne l'équipement courant du personnage donné
CREATE Procedure sp_GetEquipment(@_characterID int)
AS
Begin
	SELECT * FROM CharactersEquipment
	WHERE CharactersEquipmentCharactersID = @_characterID
End