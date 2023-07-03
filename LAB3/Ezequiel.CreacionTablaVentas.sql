use Pets
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