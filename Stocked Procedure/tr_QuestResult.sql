--met les résultats de la quête au personnage
CREATE TRIGGER tr_QuestResult
ON QuestStatus
FOR UPDATE
AS
BEGIN
	DECLARE 
		@QuestID int,--ID de la quest
		@QuestGold int,--or donnée par la quête
		@QuestExp int,--Exp donnée par la quête
		@CharID int;--id du perso

	SELECT @QuestID = QuestStatusQuestID FROM inserted 
	SELECT @CharID = QuestStatusCharactersID FROM inserted
	SELECT @QuestGold = Quest.QuestGoldReward FROM Quest WHERE QuestID = @QuestID
	SELECT @QuestExp = Quest.QuestExpReward FROM Quest WHERE QuestID = @QuestID
	
	UPDATE Characters SET CharactersGold = CharactersGold + @QuestGold, CharactersCurrentExp = CharactersCurrentExp + @QuestExp WHERE CharactersID = @CharID

END



