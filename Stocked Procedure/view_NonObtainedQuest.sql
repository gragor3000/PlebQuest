--donne une qu�te al�atoire
CREATE View view_NonObtainedQuest
As
	select Top 10 percent QuestID
	from Quest  
	where QuestID not in (select QuestStatusQuestID 
                    from QuestStatus)
	order by newid()








