use Pets
go

CREATE PROCEDURE SP_Productos_Crear
@Codigo varchar(10),
@CUIT varchar(15),
@Tipo varchar(10),
@Nombre varchar(50),
@Marca varchar(50),
@Desc varchar(50),
@Stock int,
@Precio money,
@Estado bit
AS
BEGIN
INSERT INTO Productos(CodProducto_Prod,CUITProveedor_Prod,CodTipoProducto_Prod,Nombre_Prod,Marca_Prod,Descripcion_Prod,Stock_Prod,PrecioUnitario_Prod,Estado_Prod)
SELECT @Codigo,@CUIT,@Tipo,@Nombre,@Marca,@Desc,@Stock,@Precio,@Estado
END
GO

CREATE PROCEDURE SP_Productos_DarDeBaja
@Codigo varchar(10),
@Estado bit
AS
BEGIN
UPDATE Productos
SET Estado_Prod=@Estado
where CodProducto_Prod=@Codigo
END
GO


CREATE PROCEDURE SP_Productos_ActualizarPrecio
@Codigo varchar(10),
@Precio money
AS
BEGIN
UPDATE Productos
SET PrecioUnitario_Prod=@Precio
where CodProducto_Prod=@Codigo
END
GO

CREATE PROCEDURE SP_Productos_ActualizarStock
@Codigo varchar(10),
@Stock int
AS
BEGIN
UPDATE Productos
SET Stock_Prod=@Stock
where CodProducto_Prod=@Codigo
END
GO

CREATE TRIGGER TR_Productos_PrevenirEliminar
ON Productos
INSTEAD OF DELETE
AS
BEGIN
    PRINT('No est� permitido eliminar registros de la tabla "Productos" mediante el uso de "Delete".');
    ROLLBACK;
END;