--donne le nom de L'�quipement donn�e
CREATE PROCEDURE sp_GetEquipmentTypeInfo(@TypeID int)
AS
BEGIN
	SELECT EType.ETypeName FROM EType INNER JOIN Equipment ON EquipTypeID = ETypeID 
	WHERE EquipTypeID = @TypeID
END



