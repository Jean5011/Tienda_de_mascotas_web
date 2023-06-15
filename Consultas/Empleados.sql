USE Pets
GO

-- Creamos la tabla Empleados, a partir de los datos del �ltimo DER.
CREATE TABLE Empleados (
	DNI_Em CHAR(12) NOT NULL,
	Nombre_Em VARCHAR(48) NOT NULL,
	Apellido_Em VARCHAR(48),
	Sexo_Em CHAR(2) NOT NULL DEFAULT 'M',
	FechaDeNacimiento_Em DATE NOT NULL,
	FechaDeInicio_Em DATE,
	Sueldo_Em MONEY,
	Direccion_Em VARCHAR(48),
	Provincia_Em VARCHAR(48),
	Localidad_Em VARCHAR(48),
	Nacionalidad_Em VARCHAR(48),
	Estado_Em BIT NOT NULL DEFAULT 1,
	Hash_Em VARCHAR(256) NOT NULL,
	Salt_Em VARCHAR(256) NOT NULL,
	CONSTRAINT PK_DNI_Em PRIMARY KEY (DNI_Em)
)
GO

-- Procedimientos almacenados
CREATE PROC AgregarEmpleado
@DNI CHAR(12), @NOMBRE VARCHAR(48), @APELLIDO VARCHAR(48), @SEXO CHAR(2),
@FECHANACIMIENTO DATE, @FECHAINICIO DATE, @SUELDO MONEY,
@DIRECCION VARCHAR(48), @PROVINCIA VARCHAR(48), @LOCALIDAD VARCHAR(48),
@NACIONALIDAD VARCHAR(48), @HASH VARCHAR(256), @SALT VARCHAR(256)
AS
INSERT INTO Empleados (DNI_Em, Nombre_Em, Apellido_Em, Sexo_Em, FechaDeNacimiento_Em, FechaDeInicio_Em, Sueldo_Em, Direccion_Em, Provincia_Em, Localidad_Em, Nacionalidad_Em, Hash_Em, Salt_Em)
SELECT @DNI, @NOMBRE, @APELLIDO, @SEXO, @FECHANACIMIENTO, @FECHAINICIO, @SUELDO, @DIRECCION, @PROVINCIA, @LOCALIDAD, @NACIONALIDAD, @HASH, @SALT
GO