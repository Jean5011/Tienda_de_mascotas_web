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
@Imagen varchar(100),
@Precio money,
@Estado bit
AS
BEGIN
INSERT INTO Productos(CodProducto_Prod,CUITProveedor_Prod,CodTipoProducto_Prod,Nombre_Prod,Marca_Prod,Descripcion_Prod,Stock_Prod,Imagen_Prod,PrecioUnitario_Prod,Estado_Prod)
SELECT @Codigo,@CUIT,@Tipo,@Nombre,@Marca,@Desc,@Stock,@Imagen,@Precio,@Estado
END
GO

CREATE PROCEDURE SP_Productos_ActualizarEstado
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
    PRINT('No está permitido eliminar registros de la tabla "Productos" mediante el uso de "Delete".');
    ROLLBACK;
END;
go


CREATE PROCEDURE SP_Productos_Actualizar
@Codigo varchar(10),
@CUIT varchar(15),
@Tipo varchar(10),
@Nombre varchar(50),
@Marca varchar(50),
@Desc varchar(50),
@Stock int,
@Imagen varchar(100),
@Precio money,
@Estado bit
AS
BEGIN
UPDATE Productos
SET 
CUITProveedor_Prod=@CUIT,
CodTipoProducto_Prod=@Tipo,
Nombre_Prod=@Nombre,
Marca_Prod=@Marca,
Descripcion_Prod=@Desc,
Stock_Prod=@Stock,
Imagen_Prod=@Imagen,
PrecioUnitario_Prod=@Precio,
Estado_Prod=@Estado
WHERE CodProducto_Prod=@Codigo
END
GO