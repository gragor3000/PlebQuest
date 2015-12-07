
--donne le spell qui fait le plus de dégat du personnage donnée
CREATE FUNCTION fn_SpellDmg (@CharID int)
RETURNS @Table TABLE(SpellID int, SpellDmg int)
AS
BEGIN
	DECLARE
		@Dmg int,--spell de heal qui heal le plus
		@ID int;--id du spell
	SELECT TOP 1
		@Dmg = Spell.SpellDmg,
		@ID = Spell.SpellID	

	FROM (Spell INNER JOIN SpellQuantity ON SpellID = SpellQuantitySpellID) INNER JOIN Characters ON CharactersID = SpellQuantityCharactersID
	WHERE SpellQuantityCharactersID = @CharID  AND Spell.SpellHeal > 0
	ORDER BY SpellDmg DESC
	
	INSERT INTO @Table(SpellID,SpellDmg) VALUES(@ID,@Dmg)

	RETURN
END
GO


