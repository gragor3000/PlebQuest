--gère la mort ou le level up du personnage
CREATE TRIGGER tr_Characters
ON Characters
FOR UPDATE
AS
BEGIN
	DECLARE 
		@CharID int,
		@CharHp int,
		@CharExp int,
		@CharMaxExp int;

	SELECT @CharID = CharactersID FROM inserted
	SELECT @CharHp = CharactersCurrentHP FROM inserted
	SELECT @CharExp = CharactersCurrentExp FROM inserted
	SELECT @CharMaxExp = CharactersExpNext FROM inserted
	
	if(@CharHp <= 0)--quand le joueur meurt il perd ses items et son combat
	BEGIN
		DELETE FROM CharactersEquipment WHERE CharactersEquipmentCharactersID = @CharID
		DELETE FROM Combat WHERE CombatCharactersID = @CharID
	END
	
	if(@CharExp >= @CharMaxExp)--level up du personnage
	BEGIN
		UPDATE Characters SET CharactersCurrentExp = 0
		UPDATE Characters SET CharactersLevel = CharactersLevel + 1
		--reste a faire le changement des stats et du expNext
	END
END