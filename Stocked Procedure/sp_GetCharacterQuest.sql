--donne les qu�tes du personnage donn�e
CREATE PROCEDURE [dbo].[sp_GetCharacterQuest](@CharID int)
AS
BEGIN
	SELECT Quest.QuestName, QuestStatus.QuestStatusCompleted
	FROM (Quest INNER JOIN QuestStatus ON QuestID = QuestStatusQuestID) INNER JOIN Characters ON CharactersID = QuestStatusCharactersID
	WHERE QuestStatusCharactersID = @CharID
END

GO


