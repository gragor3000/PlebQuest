--Donne les info de l'�quipment donn�e
CREATE Procedure sp_GetEquipmentInfo(@EquipID int)
As
BEGIN
	Select * FROM Equipment 
	WHERE Equipment.EquipID = @EquipID
END



