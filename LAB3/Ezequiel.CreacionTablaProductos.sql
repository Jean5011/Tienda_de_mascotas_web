use Pets
go

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
