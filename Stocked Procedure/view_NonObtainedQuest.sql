--donne une quête aléatoire
CREATE View [dbo].[view_NonObtainedQuest]
As
	select Top 10 percent * 
	from Quest  
	where QuestID not in (select QuestStatusQuestID 
                    from QuestStatus)
	order by newid()





GO


