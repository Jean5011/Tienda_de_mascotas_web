use Pets
go

CREATE PROCEDURE SP_Proveedor_Crear
@CUIT varchar(15),
@RazonSocial varchar(50),
@NombreContacto varchar(30),
@CorreoElectronico varchar(75),
@Telefono varchar(20),
@Direccion varchar(30),
@Provincia varchar(30),
@Localidad varchar(30),
@Pais varchar(20),
@CodigoPostal varchar(10)
AS
BEGIN
INSERT INTO Proveedores(CUIT_Prov,RazonSocial_Prov,NombreDeContacto_Prov,CorreoElectronico_Prov,Telefono_Prov,Direccion_Prov,Provincia_Prov,Localidad_Prov,Pais_Prov,CodigoPostal_Prov)
SELECT @CUIT,@RazonSocial,@NombreContacto,@CorreoElectronico,@Telefono,@Direccion,@Provincia,@Localidad,@Pais,@CodigoPostal
END
GO

CREATE PROCEDURE SP_Proveedores_ActualizarEstado
@CUIT varchar(15),
@Estado bit
AS
BEGIN
UPDATE Proveedores
SET Estado_Prov=@Estado
where CUIT_Prov=@CUIT
END
GO



CREATE TRIGGER TR_Proveedores_PrevenirEliminar
ON Proveedores
INSTEAD OF DELETE
AS
BEGIN
    PRINT('No está permitido eliminar registros de la tabla "Proveedores" mediante el uso de "Delete".');
    ROLLBACK;
END;
go


CREATE PROCEDURE SP_Proveedores_Actualizar
@CUIT varchar(15),
@RazonSocial varchar(50),
@NombreContacto varchar(30),
@CorreoElectronico varchar(75),
@Telefono varchar(20),
@Direccion varchar(30),
@Provincia varchar(30),
@Localidad varchar(30),
@Pais varchar(20),
@CodigoPostal varchar(10)
AS
BEGIN
UPDATE Proveedores
SET 
RazonSocial_Prov=@RazonSocial,
NombreDeContacto_Prov=@NombreContacto,
CorreoElectronico_Prov=@CorreoElectronico,
Telefono_Prov=@Telefono,
Direccion_Prov=@Direccion,
Provincia_Prov=@Provincia,
Localidad_Prov=@Localidad,
Pais_Prov=@Pais,
CodigoPostal_Prov=@CodigoPostal
WHERE CUIT_Prov=@CUIT
END
GO