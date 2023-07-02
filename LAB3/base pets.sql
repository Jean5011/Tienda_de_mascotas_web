use master
go

create database pets
go

/**** TABLAS ****/

---- tABLAS CREADAS POR JEAN ESQUEN ----

Create table animales
(

Pk_CodAnimales_An varchar(10) not null,

nombre_An char(20) not null,

NombreDeRaza_An char(20) null,

estado bit not null default 1,

Constraint PK_Animales primary key (Pk_CodAnimales_An)
)
go



create TABLE TipoDeProductos
(
PK_CodTipoProducto_TP varchar(10) NOT NULL,
CodAnimales_Tp varchar(10) NOT NULL,
TipoDeProducto_Tp varchar(15) NOT NULL CHECK(TipoDeProducto_Tp='Comida' OR TipoDeProducto_Tp='Accesorios' OR TipoDeProducto_Tp='Ropa' OR TipoDeProducto_Tp='Higiene' OR TipoDeProducto_Tp='Salud'),
Descripcion_TP text NOT NULL,
estado bit not null default 1,
Constraint PK_TipoDeProductos primary key (PK_CodTipoProducto_TP),
constraint FK_TipoDeProductosxAnimales foreign key (CodAnimales_Tp)
references Animales(Pk_CodAnimales_An)
)
go
---- tABLAS CREADAS POR JAVIER ANDRES TORALES ----

CREATE TABLE Proveedores
(
CUIT_Prov varchar(10) NOT NULL,
RazonSocial_Prov varchar(50) NOT NULL,
NombreDeContacto_Prov varchar(30) NOT NULL,
CorreoElectronico_Prov varchar(75) NOT NULL,
Telefono_Prov varchar(20) NOT NULL,
Direccion_Prov varchar(30) NOT NULL,
Provincia_Prov varchar(30) NOT NULL,
Localidad_Prov varchar(30) NOT NULL,
Pais_Prov varchar(20) NOT NULL,
CodigoPostal_Prov varchar(10) NOT NULL,
Estado_Prov bit NOT NULL DEFAULT 1,
CONSTRAINT PK_Proveedores PRIMARY KEY(CUIT_Prov)
)
go
---- tABLAS CREADAS POR MÁXIMO CANEDO ----

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

---- tABLAS CREADAS POR EZEQUIEL ALEJANDRO MARTINEZ ----

Create Table Productos
(
CodProducto_Prod varchar(10) not null,
CUITProveedor_Prod varchar(10) not null,
CodTipoProducto_Prod varchar(10) not null,
Nombre_Prod varchar(50) not null,
Marca_Prod varchar(50) not null,
Descripcion_Prod varchar(50) not null,
Stock_Prod int not null,
Imagen_Prod varchar(100) null,
PrecioUnitario_Prod money not null,
Estado_Prod bit default 1
CONSTRAINT PK_Productos PRIMARY KEY (CodProducto_Prod,CUITProveedor_Prod),

CONSTRAINT FK_Productos_CUIT FOREIGN KEY (CUITProveedor_Prod) 
references Proveedores (CUIT_Prov),

CONSTRAINT FK_Productos_TIPO FOREIGN KEY (CodTipoProducto_Prod)
references TipoDeProductos (PK_CodTipoProducto_TP)

)
go

Create Table Ventas
(
CodVenta_Vt int identity(1,1),
DNIEmpleado_Vt char(12) not null,
TipoPago_Vt varchar(50) not null,
Fecha_Vt Date not null,
PrecioTotal_Vt money not null,
CONSTRAINT PK_Ventas PRIMARY KEY (CodVenta_Vt),

CONSTRAINT FK_Ventas_DNI FOREIGN KEY (DNIEmpleado_Vt)
references Empleados (DNI_Em)
)
go

---- tABLAS CREADAS POR MARÍA OLIVIA HANCZYC  ----

CREATE TABLE DetalleDeVenta
(
CodVenta_Dv int NOT NULL,
CodProducto_Dv varchar(10) NOT NULL,
CUITProveedor_Dv varchar(15) NOT NULL,
Cantidad_Dv int NOT NULL,
PrecioUnitario_Dv money NOT NULL,
PrecioTotal_Dv money NOT NULL,
Estado_Prod bit default 1,
CONSTRAINT PK_DetalleDeVenta PRIMARY KEY (CodVenta_Dv, CodProducto_Dv, CUITProveedor_Dv),
CONSTRAINT FK_DetalleDeVenta_Ventas FOREIGN KEY (CodVenta_Dv) REFERENCES Ventas (CodVenta_Vt),
CONSTRAINT FK_DetalleDeVenta_Productos FOREIGN KEY (CodProducto_Dv, CUITProveedor_Dv) REFERENCES Productos (CodProducto_Prod, CUITProveedor_Prod)
)
GO

select  upper('Javier Andres Torales'), upper('María Olivia Hanczyc '),upper('Ezequiel Alejandro Martinez'),upper('Máximo Canedo')