--fait les actions selon les différents états du personnage
CREATE PROCEDURE sp_Action(@CharID int)
AS
BEGIN 	
	if((SELECT COUNT(*) FROM Combat) > 0)
	BEGIN
		DECLARE @TableMonster Table(Dps int,Exp int,gold int)
		INSERT INTO @TableMonster 
		SELECT * FROM fn_MobStat
		if(Combat.CombatTurn = 0)
		BEGIN
			UPDATE Characters SET CharactersCurrentHP = (CharactersCurrentHP - ((SELECT Dps FROM @TableMonster) * (SELECT CharactersLevel FROM Characters)) /2) WHERE CharactersID = @CharID
			
		END

	END
END