USE Pets
GO

CREATE TABLE DetalleDeVenta
(
CodVenta_Dv int identity(1, 1) NOT NULL,
CodProducto_Dv varchar(10) NOT NULL,
CUITProveedor_Dv varchar(15) NOT NULL,
Cantidad_Dv int NOT NULL,
PrecioUnitario_Dv money NOT NULL,
PrecioTotal_Dv money NOT NULL,
CONSTRAINT PK_DetalleDeVenta PRIMARY KEY (CodVenta_Dv, CodProducto_Dv),
CONSTRAINT FK_DetalleDeVenta_Ventas FOREIGN KEY (CodVenta_Dv) REFERENCES Ventas (CodVenta_Vt),
CONSTRAINT FK_DetalleDeVenta_Productos FOREIGN KEY (CodProducto_Dv, CUITProveedor_Dv) REFERENCES Productos (CodProducto_Prod, CUITProveedor_Prod)
)
GO