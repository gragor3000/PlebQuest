--fait mourir le personnage si sa vie est a 0 et le fait lvl up si il a l'exp nécessaire
CREATE TRIGGER tr_Characters
ON Characters
FOR UPDATE
AS
BEGIN
	DECLARE 
		@CharID int,--id du perso
		@CharHp int,--vie courante du perso
		@CharExp int,--exp du perso
		@RaceCon int,--constitution de la race du perso
		@CharCon int,--constitution du perso
		@Charlvl int,--lvl du perso
		@CharMaxExp int;--exp nécessaire au lvl up

	SELECT @CharID = CharactersID FROM inserted
	SELECT @CharHp = CharactersCurrentHP FROM inserted
	SELECT @CharExp = CharactersCurrentExp FROM inserted
	SELECT @CharMaxExp = CharactersExpNext FROM inserted
	SELECT @Charlvl = CharactersLevel FROM inserted
	SELECT @CharCon = CharactersConstitution FROM inserted
	SELECT @RaceCon = Race.RaceConstitution FROM Race INNER JOIN Characters ON CharactersRaceID = RaceID WHERE CharactersID = @CharID
	
	
	if(@CharHp <= 0)--quand le joueur meurt il perd ses items et son combat
	BEGIN
		DELETE FROM ItemQuantity WHERE ItemQuantityCharactersID = @CharID
		DELETE FROM Combat WHERE CombatCharactersID = @CharID
		UPDATE Characters SET CharactersCurrentHP = CharactersMaxHP WHERE CharactersID = @CharID
	END
	
	if(@CharExp >= @CharMaxExp)--level up du personnage
	BEGIN
		UPDATE Characters SET CharactersLevel = CharactersLevel + 1 WHERE CharactersID = @CharID
		UPDATE Characters SET CharactersExpNext = CharactersCurrentExp *(CharactersLevel ^2) WHERE CharactersID = @CharID
		UPDATE Characters SET CharactersCurrentExp = 0,
		CharactersMaxHP = CharactersMaxHP + ((@CharCon + @RaceCon) * @Charlvl) WHERE CharactersID = @CharID
		

	END
END



