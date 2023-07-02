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
select '1234567890', 'Proveedor A', 'Juan P�rez', 'juanperez@gmail.com', '123456789', 'Calle 123', 'Buenos Aires', 'Ciudad Aut�noma', 'Argentina', '1234' union
select '0987654321', 'Proveedor B', 'Mar�a L�pez', 'marialopez@hotmail.com', '987654321', 'Avenida 456', 'C�rdoba', 'C�rdoba', 'Argentina', '5678' union 
select '1112223334', 'Proveedor C', 'Pedro G�mez', 'pedrogomez@hotmail.com', '111222333', 'Plaza 789', 'Santa Fe', 'Rosario', 'Argentina', '9012' union 
select '5556667778', 'Proveedor D', 'Ana Rodr�guez', 'anarodriguez@hotmail.com', '555666777', 'Carretera 012', 'Mendoza', 'Mendoza', 'Argentina', '3456' union
select '9998887776', 'Proveedor E', 'Luis Torres', 'luistorres@outlook.com', '999888777', 'Ruta 345', 'Salta', 'Salta', 'Argentina', '7890' union
select '4443332220', 'Proveedor F', 'Carolina Mart�nez', 'carolinamartinez@gmail.com', '444333222', 'Camino 567', 'Tucum�n', 'San Miguel de Tucum�n', 'Argentina', '2345' union
select '7778889992', 'Proveedor G', 'Andr�s Castro', 'andrescastro@gmail.com', '777888999', 'Autopista 890', 'Chaco', 'Resistencia', 'Argentina', '6789' union 
select '6665554443', 'Proveedor H', 'M�nica Herrera', 'monicaherrera@gmail.com', '666555444', 'Paseo 123', 'Entre R�os', 'Paran�', 'Argentina', '0123' union 
select '2223334445', 'Proveedor I', 'Roberto S�nchez', 'robertosanchez@outlook.com', '222333444', 'Pasaje 456', 'Neuqu�n', 'Neuqu�n', 'Argentina', '4567' union
select '8887776667', 'Proveedor J', 'Marcela Fern�ndez', 'marcelafernandez@hotmail.com', '888777666', 'Galer�a 789', 'San Juan', 'San Juan', 'Argentina', '8901'
go

---- tABLAS CREADAS POR M�XIMO CANEDO ----

select  upper('Javier Andres Torales'), upper('Mar�a Olivia Hanczyc '),upper('Ezequiel Alejandro Martinez'),upper('M�ximo Canedo')