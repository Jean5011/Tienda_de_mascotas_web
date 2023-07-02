--- Trabajo Práctico Integrador de Laboratorio de Computación III.
-- Máximo Canedo, Legajo N.º 25.839



--- TABLA "EMPLEADOS"
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
    Rol_Em VARCHAR(12),
	CONSTRAINT PK_DNI_Em PRIMARY KEY (DNI_Em)
)
GO



--- TABLA "SESIONES"
CREATE TABLE Sesiones (
	CodSesion_Se INT IDENTITY(1, 1) NOT NULL,
	DNIEmpleado_Se CHAR(12) NOT NULL,
	FechaDeAlta_Se DATETIME DEFAULT GETDATE() NOT NULL,
	Token_Se VARCHAR(128) NOT NULL,
	Estado_Se BIT DEFAULT 1 NOT NULL,
	CONSTRAINT PK_Sesiones PRIMARY KEY (CodSesion_Se),
	CONSTRAINT FK_Sesiones FOREIGN KEY (DNIEmpleado_Se) REFERENCES Empleados(DNI_Em)
)
GO