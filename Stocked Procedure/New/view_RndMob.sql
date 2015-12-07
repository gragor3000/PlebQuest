

--donne un mob random
CREATE VIEW view_RndMob
AS
	select Top 10 percent MobID,MobHP from Mob  	
	order by newid()

GO


