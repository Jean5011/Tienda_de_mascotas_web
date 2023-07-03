use Pets
go

Create procedure SP_Ventas_Crear
@DNI_Empleado char(12),
@Tipo_De_Pago varchar(50),
@Fecha_Vt date,
@PrecioTotal money
AS
BEGIN
INSERT INTO Ventas (DNIEmpleado_Vt,TipoPago_Vt,Fecha_Vt,PrecioTotal_Vt)
SELECT @DNI_Empleado,@Tipo_De_Pago,@Fecha_Vt,@PrecioTotal
END
GO

Create procedure SP_Ventas_Eliminar
@CodigoVenta int
AS
BEGIN
DELETE FROM Ventas
WHERE CodVenta_Vt = @CodigoVenta
END
GO