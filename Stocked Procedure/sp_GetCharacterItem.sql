--donne les items du personnage donnée
CREATE PROCEDURE sp_GetCharacterItem(@CharID int)
As
BEGIN
	SELECT Item.ItemID, Item.ItemName, ItemQuantity.ItemQuantityQuantity
	FROM (Item INNER JOIN ItemQuantity ON ItemID = ItemQuantityItemID) INNER JOIN Characters ON ItemQuantityCharactersID = CharactersID
	WHERE ItemQuantityCharactersID = @CharID
END