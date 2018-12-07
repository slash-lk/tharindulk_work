CREATE PROCEDURE GetPetName
	@carID		INT,
	@petName	CHAR(10) OUTPUT
AS
	SELECT @petName = PetName 
	FROM Inventory 
	WHERE CarId = @carID;