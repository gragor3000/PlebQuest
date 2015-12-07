
--donne le meilleur spell de soin du personnage donnée
CREATE FUNCTION fn_HealSpell (@CharID int)
RETURNS @Table TABLE(SpellID int, SpellHeal int)
AS
BEGIN
	DECLARE
		@Heal int,--spell de heal qui heal le plus
		@ID int;--id du spell
	SELECT TOP 1
		@Heal = Spell.SpellHeal,
		@ID = Spell.SpellID

	FROM (Spell INNER JOIN SpellQuantity ON SpellID = SpellQuantitySpellID) INNER JOIN Characters ON CharactersID = SpellQuantityCharactersID
	WHERE SpellQuantityCharactersID = @CharID  AND Spell.SpellHeal > 0
	ORDER BY SpellHeal DESC
	
	INSERT INTO @Table(SpellID,SpellHeal) VALUES(@ID,@Heal)

	RETURN
END

GO


