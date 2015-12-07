--donne les stat du monstre donnée
CREATE FUNCTION fn_MobStat(@MobID int)
RETURNS @_Table TABLE(MobDps int,MobExp int,MobGold int)
AS
BEGIN
	DECLARE 
		@Dps int,
		@Exp int,
		@Gold int;
	SELECT
		@Dps = Mob.MobDPS,
		@Exp = Mob.MobExpReward,
		@Gold = Mob.MobGoldReward
	FROM Mob
	WHERE MobID = @MobID

	INSERT INTO @_Table(MobDps,MobExp,MobGold) VALUES(@Dps,@Exp,@Gold)
RETURN	
END