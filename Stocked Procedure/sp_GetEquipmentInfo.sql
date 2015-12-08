--Donne les info de l'équipment donnée
CREATE Procedure sp_GetEquipmentInfo(@EquipID int)
As
BEGIN
	Select * FROM Equipment 
	WHERE Equipment.EquipID = @EquipID
END



