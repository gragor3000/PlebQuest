
--donne les récompenses si le monstre est mort
CREATE TRIGGER tr_MonsterCombat
ON Combat
FOR UPDATE 
AS
BEGIN
	DECLARE 
		@MonsterID int,--id du Monstre en combat
		@MonsterExp int,--exp donnée par le Monstre en combat
		@MonsterGold int,--or donnée par le Monstre en combat
		@MonsterDrop int,--item que le Monstre drop
		@CharID int,--id du perso en combat
		@CharLvl int,--niveau du perso en combat
		@MonsterHp int;--vie du Monstre en combat

	SELECT @MonsterID = CombatMonsterID FROM inserted
	SELECT @MonsterHp = CombatMonsterHP FROM inserted
	SELECT @CharID = CombatCharactersID FROM inserted
	SELECT @CharLvl = Characters.CharactersLevel FROM Characters WHERE CharactersID = @CharID

	if(@MonsterHp <= 1 OR @MonsterHp is NUll)--si le monstre est mort
	BEGIN
		SELECT @MonsterExp = Mob.MobExpReward FROM Mob WHERE MobID = @MonsterID
		SELECT @MonsterGold = Mob.MobGoldReward FROM Mob WHERE MobID = @MonsterID		
		DECLARE @ItemID int
		SET @ItemID = 4
		DECLARE CurDrop CURSOR FOR SELECT MobDrop.MobDropItemID FROM MobDrop WHERE MobDropMobID = @MonsterID
		OPEN CurDrop

		FETCH NEXT FROM CurDrop INTO @ItemID
		WHILE @@FETCH_STATUS = 0
		BEGIN
			INSERT INTO ItemQuantity(ItemQuantityQuantity,ItemQuantityItemID,ItemQuantityCharactersID) VALUES(1,@ItemID,@CharID)
			FETCH NEXT FROM CurDrop INTO @ItemID
		END
		CLOSE CurDrop
		DEALLOCATE CurDrop
		UPDATE Characters SET CharactersCurrentExp = @MonsterExp * @CharLvl + CharactersCurrentExp, CharactersGold = @MonsterGold * @CharLvl + CharactersGold, CharactersCurrentHP = CharactersMaxHP WHERE CharactersID = @CharID
		DELETE FROM Combat WHERE CombatCharactersID = @CharID		
	END
END


