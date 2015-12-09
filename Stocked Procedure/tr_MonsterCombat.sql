
--donne les récompenses si le monstre est mort
CREATE TRIGGER tr_MonsterCombat
ON Combat
FOR UPDATE 
AS
BEGIN
	DECLARE 
		@MonsterID int,
		@MonsterExp int,
		@MonsterGold int,
		@MonsterDrop int,
		@CharID int,
		@CharLvl int,
		@MonsterHp int;

	SELECT @MonsterID = CombatMonsterID FROM inserted
	SELECT @MonsterHp = CombatMonsterHP FROM inserted
	SELECT @CharID = CombatCharactersID FROM inserted
	SELECT @CharLvl = Characters.CharactersLevel FROM Characters WHERE CharactersID = @CharID

	if(@MonsterHp <= 1 OR @MonsterHp is NUll)
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
GO


