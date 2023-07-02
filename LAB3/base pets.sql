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
CUIT_Prov varchar(10) NOT NULL,RazonSocial_Prov varchar(50) NOT NULL,
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
--insertar 10 registros

insert into Proveedores (CUIT_Prov,RazonSocial_Prov,NombreDeContacto_Prov,CorreoElectronico_Prov,Telefono_Prov,Dirreccion_Prod,Provincia_Prod,Localidad_Prod,Pais_Prod,CodigoPostal_Prov)
select '1234567890', 'Proveedor A', 'Juan Pérez', 'juanperez@gmail.com', '123456789', 'Calle 123', 'Buenos Aires', 'Ciudad Autónoma', 'Argentina', '1234' union
select '0987654321', 'Proveedor B', 'María López', 'marialopez@hotmail.com', '987654321', 'Avenida 456', 'Córdoba', 'Córdoba', 'Argentina', '5678' union 
select '1112223334', 'Proveedor C', 'Pedro Gómez', 'pedrogomez@hotmail.com', '111222333', 'Plaza 789', 'Santa Fe', 'Rosario', 'Argentina', '9012' union 
select '5556667778', 'Proveedor D', 'Ana Rodríguez', 'anarodriguez@hotmail.com', '555666777', 'Carretera 012', 'Mendoza', 'Mendoza', 'Argentina', '3456' union
select '9998887776', 'Proveedor E', 'Luis Torres', 'luistorres@outlook.com', '999888777', 'Ruta 345', 'Salta', 'Salta', 'Argentina', '7890' union
select '4443332220', 'Proveedor F', 'Carolina Martínez', 'carolinamartinez@gmail.com', '444333222', 'Camino 567', 'Tucumán', 'San Miguel de Tucumán', 'Argentina', '2345' union
select '7778889992', 'Proveedor G', 'Andrés Castro', 'andrescastro@gmail.com', '777888999', 'Autopista 890', 'Chaco', 'Resistencia', 'Argentina', '6789' union 
select '6665554443', 'Proveedor H', 'Mónica Herrera', 'monicaherrera@gmail.com', '666555444', 'Paseo 123', 'Entre Ríos', 'Paraná', 'Argentina', '0123' union 
select '2223334445', 'Proveedor I', 'Roberto Sánchez', 'robertosanchez@outlook.com', '222333444', 'Pasaje 456', 'Neuquén', 'Neuquén', 'Argentina', '4567' union
select '8887776667', 'Proveedor J', 'Marcela Fernández', 'marcelafernandez@hotmail.com', '888777666', 'Galería 789', 'San Juan', 'San Juan', 'Argentina', '8901'
go

---- tABLAS CREADAS POR MÁXIMO CANEDO ----

select  upper('Javier Andres Torales'), upper('María Olivia Hanczyc '),upper('Ezequiel Alejandro Martinez'),upper('Máximo Canedo')