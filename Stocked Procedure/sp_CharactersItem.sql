--Donne les items et la quantity du character donn�e
CREATE PROCEDURE sp_CharactersItem(@CharactersID int)
AS
BEGIN
	SELECT Item.ItemID, Item.ItemName,Item.ItemValue,ItemQuantity.ItemQuantityQuantity
	FROM (Item INNER JOIN ItemQuantity ON ItemID = ItemQuantityItemID) INNER JOIN Characters ON CharactersID = ItemQuantityCharactersID
	WHERE ItemQuantityCharactersID = @CharactersID AND ItemID in(Select ItemQuantityItemID from ItemQuantity)
END