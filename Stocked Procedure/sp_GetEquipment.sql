--donne l'�quipement courant du personnage donn�
CREATE Procedure sp_GetEquipment(@_characterID int)
AS
Begin
	SELECT * FROM CharactersEquipment
	WHERE CharactersEquipmentCharactersID = @_characterID
End