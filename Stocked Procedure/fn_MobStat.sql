--donne les stat du monstre donn�e
CREATE FUNCTION fn_MobStat(@MobID int)
RETURNS @_Table TABLE(MobDps int,MobExp int,MobGold int)
AS
BEGIN
	DECLARE 
		@Dps int,--d�gat du monstre
		@Exp int,--exp donn�e par le monstre
		@Gold int;--gold donn�e par le monstre
	SELECT
		@Dps = Mob.MobDPS,
		@Exp = Mob.MobExpReward,
		@Gold = Mob.MobGoldReward
	FROM Mob
	WHERE MobID = @MobID

	INSERT INTO @_Table(MobDps,MobExp,MobGold) VALUES(@Dps,@Exp,@Gold)
RETURN	
END



