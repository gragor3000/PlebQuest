--met les r�sultats de la qu�te au personnage
CREATE TRIGGER tr_QuestResult
ON QuestStatus
FOR UPDATE
AS
BEGIN
	DECLARE 
		@QuestID int,--ID de la quest
		@QuestGold int,--or donn�e par la qu�te
		@QuestExp int,--Exp donn�e par la qu�te
		@CharID int;--id du perso

	SELECT @QuestID = QuestStatusQuestID FROM inserted 
	SELECT @CharID = QuestStatusCharactersID FROM inserted
	SELECT @QuestGold = Quest.QuestGoldReward FROM Quest WHERE QuestID = @QuestID
	SELECT @QuestExp = Quest.QuestExpReward FROM Quest WHERE QuestID = @QuestID
	
	UPDATE Characters SET CharactersGold = CharactersGold + @QuestGold, CharactersCurrentExp = CharactersCurrentExp + @QuestExp WHERE CharactersID = @CharID

END



