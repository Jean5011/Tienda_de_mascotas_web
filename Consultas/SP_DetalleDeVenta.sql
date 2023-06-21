USE Pets
GO

CREATE PROCEDURE SP_DetalleDeVenta_Agregar
@CodigoVenta int,
@CodigoProducto varchar(10),
@CUITProveedor varchar(15),
@Cantidad int
AS
	BEGIN
		DECLARE @PrecioUnitario money

		--Obtener el precio unitario de la tabla Productos:
		SELECT @PrecioUnitario = PrecioUnitario_Prod
		FROM Productos
		WHERE CodProducto_Prod = @CodigoProducto

		--Insertar el registro en la tabla DetalleDeVenta:
		INSERT INTO DetalleDeVenta (CodVenta_Dv, CodProducto_Dv, CUITProveedor_Dv, Cantidad_Dv, PrecioUnitario_Dv, PrecioTotal_Dv, Estado_Dv)
		SELECT @CodigoVenta, @CodigoProducto, @CUITProveedor, @Cantidad, @PrecioUnitario, (@Cantidad * @PrecioUnitario), 1
	END
GO 



CREATE TRIGGER TR_actualizarStockProductos
ON DetalleDeVenta AFTER INSERT AS
	BEGIN 
		SET NOCOUNT ON
		UPDATE Productos
		SET Stock_Prod = Stock_Prod - (SELECT Cantidad_Dv FROM INSERTED)
		WHERE CodProducto_Prod = (SELECT CodProducto_Dv FROM INSERTED) AND
		CUITProveedor_Prod = (SELECT CUITProveedor_Dv FROM INSERTED)
	END
GO

CREATE PROCEDURE SP_DetalleDeVentas_DarDeBaja
@CodigoVenta int
AS
BEGIN
UPDATE DetalleDeVenta 
SET Estado_Dv = 0
WHERE CodVenta_Dv = @CodigoVenta
END
GO