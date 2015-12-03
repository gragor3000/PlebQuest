CREATE Table Combat
(
	CombatID int IDENTITY(0,1),
	CombatCharactersID int NOT NULL,
	CombatMonsterID int NOT NULL,
	CombatMonsterHP int NOT NULL,
	CombatTurn BIT NOT NULL,
	
	CONSTRAINT CCombatID PRIMARY KEY(CombatID),
	CONSTRAINT CCombatCharactersID FOREIGN KEY(CombatCharactersID) REFERENCES Characters(CharactersID),
	CONSTRAINT CCombatMonsterID FOREIGN KEY(CombatMonsterID) REFERENCES Mob(MobID),	
)
