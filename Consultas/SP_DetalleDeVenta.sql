USE Pets
GO

--Procedimientos:
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

CREATE PROCEDURE SP_DetalleDeVentas_DarDeBaja
@CodigoVenta int
AS
BEGIN
UPDATE DetalleDeVenta 
SET Estado_Dv = 0
WHERE CodVenta_Dv = @CodigoVenta
END
GO

--Este procedimiento disminuye la cantidad en la tabla DetalleDeVenta, y aumenta el stock en la tabla Productos:
CREATE PROCEDURE SP_DetalleDeVenta_disminuirCantidadVendida
@CodigoProducto varchar(10), @CUITProveedor varchar(15), --Variables relacionadas con Productos y DetalleDeVenta.
@CodigoVenta int --Variable relacionada solo con DetalleDeVenta.
AS
DECLARE @Stock int, @Resultado bit --(Resultado que devuelve el procedimiento).
SELECT @Stock = Stock_Prod FROM Productos WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor
IF(@Stock > 0) --Si el stock es mayor a cero, entonces:
	BEGIN
		UPDATE DetalleDeVenta
		SET Cantidad_Dv = Cantidad_Dv - 1 --Se le resta una unidad a la cantidad.
		WHERE CodVenta_Dv = @CodigoVenta AND CodProducto_Dv = @CodigoProducto AND CUITProveedor_Dv = @CUITProveedor

		UPDATE Productos
		SET Stock_Prod = Stock_Prod + 1 --Se actualiza el stock en la tabla Productos, sumándole una unidad (ya que se vende menos cantidad).
		WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor

		SET @Resultado = 1
		SELECT @Resultado AS RESULTADO
	END
ELSE --En cambio, si el stock es menor o igual a cero:
	BEGIN
		SET @Resultado = 0
		SELECT @Resultado AS RESULTADO
	END
GO

--Este procedimiento aumenta la cantidad en la tabla DetalleDeVenta, y disminuye el stock en la tabla Productos:
CREATE PROCEDURE SP_DetalleDeVenta_aumentarCantidadVendida
@CodigoProducto varchar(10), @CUITProveedor varchar(15), --Variables relacionadas con Productos y DetalleDeVenta.
@CodigoVenta int --Variable relacionada solo con DetalleDeVenta.
AS
DECLARE @Stock int, @Resultado bit --(Resultado que devuelve el procedimiento).
SELECT @Stock = Stock_Prod FROM Productos WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor
IF(@Stock > 0) --Si el stock es mayor a cero, entonces:
	BEGIN
		UPDATE DetalleDeVenta
		SET Cantidad_Dv = Cantidad_Dv + 1 --Se le suma una unidad a la cantidad.
		WHERE CodVenta_Dv = @CodigoVenta AND CodProducto_Dv = @CodigoProducto AND CUITProveedor_Dv = @CUITProveedor

		UPDATE Productos
		SET Stock_Prod = Stock_Prod - 1 --Se actualiza el stock en la tabla Productos, restándole una unidad (ya que se vende más cantidad).
		WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor

		SET @Resultado = 1
		SELECT @Resultado AS RESULTADO
	END
ELSE --En cambio, si el stock es menor o igual a cero:
	BEGIN
		SET @Resultado = 0
		SELECT @Resultado AS RESULTADO
	END
GO

--Trigger:
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