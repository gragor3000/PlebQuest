--donne une quête non obtenue par le joueur donnée
--doit ajouter le order by chiffre random
CREATE FUNCTION fn_NonObtainedQuest(@CharID int)
RETURNS @Table TABLE(QuestID int)
AS
BEGIN
	DECLARE @Result int--id de la quête au hasard
	SELECT TOP 10 percent @Result = QuestID
	from Quest
	where QuestID not in (select QuestStatusQuestID 
                    from QuestStatus WHERE  QuestStatusCharactersID = @CharID) 
	INSERT INTO @Table(QuestID) VALUES(@Result)

	RETURN 
END



