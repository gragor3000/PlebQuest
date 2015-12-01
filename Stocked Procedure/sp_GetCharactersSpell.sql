--donne les spells et leurs quantité du personnage donnée
CREATE PROCEDURE sp_GetCharacterSpell(@CharID int)
AS
Begin
	SELECT Spell.SpellID, Spell.SpellName, SpellQuantity.SpellQuantityQuantity
	FROM (Spell INNER JOIN SpellQuantity ON SpellID = SpellQuantitySpellID) INNER JOIN Characters ON CharactersID = SpellQuantityCharactersID
	WHERE SpellQuantityCharactersID = @CharID
End