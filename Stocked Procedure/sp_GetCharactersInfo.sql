--donne les stats du character
Create Procedure sp_GetCharacterInfo(@CharacterID int)
AS
BEGIN
	Select * FROM Characters
	WHERE Characters.CharactersID = @CharacterID
END



