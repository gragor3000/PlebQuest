
--fait les actions selon les différents états du personnage
CREATE PROCEDURE sp_Action(@CharID int)
AS
BEGIN
	 	
	if((SELECT COUNT(*) FROM Combat WHERE CombatCharactersID = @CharID) > 0)
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
			if(@Hp < 0)
			BEGIN
				SET @Hp = 0
			END
			UPDATE Characters SET CharactersCurrentHP = @Hp  WHERE CharactersID = @CharID
			UPDATE Combat Set CombatTurn = 1 Where CombatCharactersID = @CharID
		END
		Else--attack du perso
		Begin
			DECLARE @Random int
			Set @Random = ROUND(RAND() * 100,0)
			DECLARE @MaxHp int
			SELECT @MaxHp = Characters.CharactersMaxHP FROM Characters WHERE CharactersID = @CharID

			if(@Hp <= ((0.50) * @MaxHp))--spell de soin
			BEGIN
				DECLARE @HealSpell TABLE(SpellID int,SpellHeal int)
				INSERT INTO @HealSpell 
				SELECT * FROM fn_HealSpell(@CharID)
				UPDATE Characters SET CharactersCurrentHP = CharactersCurrentHP + (Select SpellHeal FROM @HealSpell) WHERE CharactersID = @CharID
				UPDATE SpellQuantity SET SpellQuantityQuantity = SpellQuantityQuantity - 1 WHERE SpellQuantityCharactersID = @CharID AND SpellQuantitySpellID = (Select SpellID FROM @HealSpell)
			END

			Else if(@Random <= 30)--spell d'Attack
			BEGIN
				DECLARE @DmgSpell TABLE(SpellID int,SpellDmg int)
				INSERT INTO @DmgSpell
				SELECT * FROM fn_SpellDmg(@CharID)

				if((((SELECT CombatMonsterHP from Combat WHERE CombatCharactersID = @CharID) - (SELECT SpellDmg FROM @DmgSpell)) <= 1) AND ((SELECT SpellDmg FROM @DmgSpell) is not null))
				BEGIN
					UPDATE Combat SET CombatMonsterHP = 1 WHERE CombatCharactersID = @CharID 
				END
				ELSE
				Begin
					UPDATE Combat SET CombatMonsterHP = CombatMonsterHP - (SELECT SpellDmg FROM @DmgSpell) WHERE CombatCharactersID = @CharID
				END
				UPDATE SpellQuantity SET SpellQuantityQuantity = SpellQuantityQuantity - 1 WHERE SpellQuantityCharactersID = @CharID AND SpellQuantitySpellID = (Select SpellID FROM @DmgSpell)
			END

			ELSE--attack normal
			BEGIN
				DECLARE @EquipID int
				DECLARE @PersoDmg int
				Set @PersoDmg = 0
				DECLARE CurDmgs CURSOR FOR SELECT CharactersEquipment.CharactersEquipmentEquipmentID FROM CharactersEquipment
				WHERE CharactersEquipment.CharactersEquipmentCharactersID = @CharID
				OPEN CurDmgs

				FETCH NEXT FROM CurDmgs INTO @EquipID
				WHILE @@FETCH_STATUS = 0
				BEGIN
					DECLARE @EquipStat int
					SELECT @EquipStat = Equipment.EquipStrength FROM Equipment WHERE EquipID = @EquipID
					Set @PersoDmg = @PersoDmg + @EquipStat
					FETCH NEXT FROM CurDmgs INTO @EquipID 
				END
				CLOSE CurDmgs
				DEALLOCATE CurDmgs
				DECLARE @RaceStr int
				DECLARE @CharStr int
				SELECT @CharStr = Characters.CharactersStrength FROM Characters WHERE CharactersID = @CharID
				Set @PersoDmg = @CharStr + @PersoDmg
				SELECT @RaceStr = Race.RaceStrength FROM Race INNER JOIN Characters ON CharactersRaceID = Race.RaceID WHERE CharactersID = @CharID 
				SET @PersoDmg = @RaceStr + @PersoDmg

				if(((SELECT CombatMonsterHP from Combat WHERE CombatCharactersID = @CharID) - @PersoDmg) <= 1 )
				BEGIN
					UPDATE Combat SET CombatMonsterHP = 1 WHERE CombatCharactersID = @CharID
				END
				ELSE
				BEGIN
					UPDATE Combat SET CombatMonsterHP = (CombatMonsterHP - @PersoDmg) WHERE CombatCharactersID = @CharID
				END
			END
			UPDATE Combat Set CombatTurn = 0 WHERE CombatCharactersID = @CharID
		END
	END

	ELSE--pas en combat
	BEGIN
		DECLARE @ItemCount int
		DECLARE @ItemID int
		SET @ItemCount = 0
		DECLARE CurItem CURSOR FOR SELECT ItemQuantity.ItemQuantityItemID FROM ItemQuantity WHERE ItemQuantityCharactersID = @CharID
		OPEN CurItem

		FETCH NEXT FROM CurItem INTO @ItemID
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			DECLARE @Quantity int
			SELECT @Quantity = ItemQuantityQuantity FROM ItemQuantity WHERE ItemQuantityItemID = @ItemID
			SET @ItemCount = @ItemCount + @Quantity
			FETCH NEXT FROM CurItem INTO @ItemID
		END
		CLOSE CurItem
		DEALLOCATE CurItem

		if(@ItemCount < ((0.80) * 255))--si il peut encore continuer a se battre(pas encore plein)
		BEGIN
			Declare @MobID int
			Declare @MobHp int
			DECLARE @MobTable TABLE(MobID int,MobHp int)			
			INSERT INTO @MobTable
			SELECT * FROM view_RndMob	
			SELECT @MobID = MobID FROM @MobTable
			SELECT @MobHp = MobHp FROM @MobTable
			INSERT INTO Combat(CombatCharactersID,CombatMonsterID,CombatMonsterHP,CombatTurn) VALUES(@CharID,@MobID,@MobHp,1)		
		END
		ELSE
		BEGIN --plein donc retourne au village
			DECLARE @QuestID int
			DECLARE @QuestItemID int
			SELECT @QuestID = QuestStatusQuestID FROM QuestStatus WHERE QuestStatusCharactersID = @CharID AND QuestStatusCompleted = 0
			SELECT @QuestItemID = QuestItemID FROM Quest WHERE QuestID = @QuestID
			
			--Quest Item a rajouté dans table Quest

			--accomplie la quête
			if((SELECT COUNT(*) FROM ItemQuantity WHERE ItemQuantityCharactersID = @CharID AND ItemQuantityItemID = @QuestItemID) > 0) 
			BEGIN
				UPDATE QuestStatus SET QuestStatusCompleted = 1 WHERE QuestStatusCharactersID = @CharID
				DELETE FROM ItemQuantity WHERE ItemQuantityCharactersID = @CharID AND ItemQuantityItemID = @QuestItemID
				SELECT @QuestID = QuestID FROM fn_NonObtainedQuest(76)
				INSERT INTO QuestStatus(QuestStatusObtained,QuestStatusCompleted,QuestStatusQuestID,QuestStatusCharactersID) VALUES(1,0,@QuestID,@CharID)
			END

			--vidage d'inventaire
			DECLARE CurItem CURSOR FOR SELECT ItemQuantityItemID FROM ItemQuantity WHERE ItemQuantityCharactersID = @CharID
			OPEN CurItem

			FETCH NEXT FROM CurItem INTO @ItemID
			WHILE @@FETCH_STATUS = 0
			BEGIN 
				DECLARE @ItemGold int
				DECLARE @ItemQuantity int
				SELECT @ItemQuantity = ItemQuantityQuantity FROM ItemQuantity WHERE ItemQuantityItemID = @ItemID
				SELECT @ItemGold = Item.ItemValue FROM Item WHERE ItemID = @ItemID
				UPDATE Characters SET CharactersGold = @ItemGold*@ItemQuantity +  CharactersGold 
				DELETE FROM ItemQuantity WHERE ItemQuantityItemID = @ItemID AND ItemQuantityCharactersID = @CharID
				FETCH NEXT FROM CurItem INTO @ItemID
			END
			CLOSE CurItem
			DEALLOCATE CurItem
			
			--Achat d'équipement
			DECLARE @NewEquip int
			DECLARE @EquipType int		

			SELECT @NewEquip = EquipID FROM fn_BuyEquipment(@CharID)

			if(@NewEquip <> Null)
			Begin
				SELECT @EquipType = Equipment.EquipTypeID FROM Equipment WHERE EquipID = @NewEquip

			-- a changer donne pas équipement en fonction de sa class donne n'importe quelle type d'équipement

				if((SELECT Count(*) FROM CharactersEquipment INNER JOIN Equipment ON CharactersEquipmentEquipmentID = EquipID WHERE EquipTypeID = @EquipType) = 0)
				BEGIN
					INSERT INTO CharactersEquipment(CharactersEquipmentEquipmentID,CharactersEquipmentCharactersID) VALUES(@NewEquip,@CharID)
				END
				ELSE
				BEGIN
					DECLARE @OldEquip int
					SELECT @OldEquip = Equipment.EquipID FROM CharactersEquipment INNER JOIN Equipment ON CharactersEquipmentEquipmentID = EquipID WHERE EquipTypeID = @EquipType
					UPDATE CharactersEquipment SET CharactersEquipmentEquipmentID = @NewEquip WHERE CharactersEquipmentEquipmentID = @OldEquip
				END
			END	
		END
	END		
END



