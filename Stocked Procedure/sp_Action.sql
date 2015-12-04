--fait les actions selon les différents états du personnage
CREATE PROCEDURE sp_Action(@CharID int)
AS
BEGIN
	 	
	if((SELECT COUNT(*) FROM Combat) > 0)
	BEGIN
		DECLARE @TableMonster Table(Dps int,Exps int,gold int)
		DECLARE @Turn bit
		DECLARE @MonsterID int
		DECLARE @Hp int
		Select @Hp =  CharactersCurrentHP FROM Characters WHERE CharactersID = @CharID
		Select TOP 1 @MonsterID = Combat.CombatMonsterID FROM Combat
		SELECT TOP 1 @Turn = Combat.CombatTurn FROM Combat
		INSERT INTO @TableMonster 
		SELECT * FROM fn_MobStat(@MonsterID)		

		if(@Turn = 0)
		BEGIN--Attack du monstre			
			Set @Hp = @Hp - ((SELECT Dps FROM @TableMonster) * (SELECT CharactersLevel FROM Characters WHERE CharactersID = @CharID)) /2
			UPDATE Characters SET CharactersCurrentHP = @Hp  WHERE CharactersID = @CharID
		END
		Else--attack du perso
		Begin
			DECLARE @Random int
			Set @Random = ROUND(RAND() * 100,0)
			DECLARE @MaxHp int
			SELECT @MaxHp = Characters.CharactersMaxHP FROM Characters WHERE CharactersID = @CharID

			if(@Hp <= ((10/100) * @MaxHp))--spell de soin
			BEGIN
				DECLARE @HealSpell TABLE(SpellID int,SpellHeal int)
				INSERT INTO @HealSpell 
				SELECT * FROM fn_HealSpell(@CharID)
				UPDATE Characters SET CharactersCurrentHP = CharactersCurrentHP + (Select SpellHeal FROM @HealSpell) WHERE CharactersID = @CharID
				UPDATE SpellQuantity SET SpellQuantityQuantity = SpellQuantityQuantity - 1 WHERE SpellQuantityCharactersID = @CharID
			END
			Else if(@Random <= 30)--spell d'Attack
			BEGIN
				DECLARE @DmgSpell TABLE(SpellID int,SpellDmg int)
				INSERT INTO @DmgSpell
				SELECT * FROM fn_SpellDmg(@CharID)
				UPDATE Combat SET CombatMonsterHP = CombatMonsterHP - (SELECT SpellDmg FROM @DmgSpell) WHERE CombatCharactersID = @CharID
				UPDATE SpellQuantity SET SpellQuantityQuantity = SpellQuantityQuantity - 1 WHERE SpellQuantityCharactersID = @CharID
			END
			ELSE--attack normal
			BEGIN
				DECLARE @EquipID int
				DECLARE @PersoDmg int
				DECLARE CurDmg CURSOR FOR SELECT CharactersEquipment.CharactersEquipmentEquipmentID FROM CharactersEquipment
				WHERE CharactersEquipment.CharactersEquipmentCharactersID = @CharID
				OPEN CurDmg

				FETCH NEXT FROM CurDmg INTO @EquipID
				WHILE @@FETCH_STATUS = 0
				BEGIN
					DECLARE @EquipStat int
					SELECT @EquipStat = Equipment.EquipStrength FROM Equipment WHERE EquipID = @EquipID
					Set @PersoDmg = @PersoDmg + @EquipStat
					FETCH NEXT FROM CurDmg INTO @EquipID 
				END
				CLOSE CurDmg
				DEALLOCATE CurCom
				SELECT @PersoDmg = Characters.CharactersStrength FROM Characters WHERE CharactersID = @CharID + @PersoDmg
				SELECT @PersoDmg = Race.RaceStrength FROM Race INNER JOIN Characters ON CharactersRaceID = Race.RaceID
			END
		END
	END
END