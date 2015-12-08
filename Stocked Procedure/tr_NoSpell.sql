--supprime un spell si il en reste plus
CREATE TRIGGER tr_NoSpell
ON SpellQuantity
FOR UPDATE
AS
BEGIN
	DECLARE @SpellQuantity int
	SELECT @SpellQuantity = SpellQuantityQuantity FROM inserted
	DECLARE @SpellID int
	SELECT @SpellID = SpellQuantitySpellID FROM inserted
	if(@SpellQuantity <=0)
	BEGIN
		DELETE FROM SpellQuantity WHERE SpellQuantitySpellID = @SpellID
	END
END