USE Pets
GO

-- Creamos la tabla Empleados, a partir de los datos del último DER.
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
