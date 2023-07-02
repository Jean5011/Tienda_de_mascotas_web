--- Trabajo Práctico Integrador de Laboratorio de Computación III.
-- Máximo Canedo, Legajo N.º 25.839



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Añade un registro a la tabla Empleados.
-- Uso: /Empleados/CrearCuenta.aspx
CREATE PROCEDURE CrearEmpleado
	@DNI char(12),
	@NOMBRE varchar(48),
	@APELLIDO varchar(48),
	@SEXO char(2),
	@FECHANACIMIENTO date,
	@FECHAINICIO date,
	@SUELDO money,
	@DIRECCION varchar(48),
	@PROVINCIA varchar(48),
	@LOCALIDAD varchar(48),
	@NACIONALIDAD varchar(48),
	@HASH varchar(48),
	@SALT varchar(48),
	@ROL varchar(12)
    AS 
    BEGIN
        INSERT INTO [Empleados] 
            (DNI_Em, Nombre_Em, Apellido_Em, Sexo_Em, FechaDeNacimiento_Em, FechaDeInicio_Em, Sueldo_Em, Direccion_Em, Provincia_Em, Localidad_Em, Nacionalidad_Em, Estado_Em, Hash_Em, Salt_Em, Rol_Em)
            SELECT 
                @DNI, @NOMBRE, @APELLIDO, @SEXO, @FECHANACIMIENTO, @FECHAINICIO, @SUELDO, @DIRECCION, @PROVINCIA, @LOCALIDAD, @NACIONALIDAD, 1, @HASH, @SALT, @ROL
    END
GO



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Cambia el hash y el salt de un empleado en particular.
-- Uso: Página /Empleados/CambiarClave.aspx.
CREATE PROC CambiarClave 
    @DNI CHAR(12), 
    @HASH VARCHAR(256), 
    @SALT VARCHAR(256) 
    AS
    BEGIN
        SET NOCOUNT ON
        UPDATE Empleados 
            SET 
                Hash_Em = @HASH, 
                Salt_Em = @SALT 
            WHERE 
                DNI_Em = @DNI
    END
GO



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Cambia el sueldo de un registro Empleado. Devuelve 1 si se pudo, o 0 si no.
CREATE PROC CambiarSueldo 
    @DNI CHAR(12), 
    @NUEVOSUELDO MONEY, 
    @RESULTADO BIT OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON
        UPDATE Empleados SET Sueldo_Em = @NUEVOSUELDO
        WHERE DNI_Em = @DNI

        IF @@ROWCOUNT > 0
            SET @RESULTADO = 1
        ELSE 
            SET @RESULTADO = 0

        RETURN
    END
GO



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Deshabilita un registro Empleado. Devuelve 1 si se pudo, o 0 si no.
CREATE PROC DeshabilitarEmpleado 
    @DNI CHAR(10), 
    @RESULTADO BIT OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON
        UPDATE Empleados SET Estado_Em = 0
        WHERE DNI_Em = @DNI

        IF @@ROWCOUNT > 0
            SET @RESULTADO = 1
        ELSE
            SET @RESULTADO = 0

        RETURN
    END
GO



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Devuelve la cantidad y los detalles del producto más vendido en los últimos siete días.
-- Observaciones: Dado que el servidor usa la hora de Greenwich y no es posible cambiarlo, les resté 3 horas a los cálculos.
-- Uso: Widget de Productos en página de Inicio (/Inicio.aspx).
CREATE PROCEDURE Widget_ProductoMasVendido_UltimaSemana AS
BEGIN
    SELECT 
        D.Cantidad, 
        Productos.* 
    FROM Productos
        INNER JOIN (
            SELECT TOP(1) 
                SUM(Cantidad_Dv) as [Cantidad], 
                CodProducto_Dv, 
                CUITProveedor_Dv
            FROM DetalleDeVenta 
                INNER JOIN Ventas
                ON DetalleDeVenta.CodVenta_Dv = Ventas.CodVenta_Vt
            WHERE 
                Ventas.Fecha_Vt >= DATEADD(day, -171, GETDATE()) 
                AND 
                Ventas.Fecha_Vt <= DATEADD(hour, -3, GETDATE())
            GROUP BY CodProducto_Dv, CUITProveedor_Dv
            ORDER BY [Cantidad] DESC
            ) AS D
        ON 
            D.CodProducto_Dv = CodProducto_Prod 
            AND 
            D.CUITProveedor_Dv = CUITProveedor_Prod
END



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Devuelve el total de las ventas de las últimas 24 horas.
-- Observaciones: Dado que el servidor usa la hora de Greenwich y no es posible cambiarlo, les resté 3 horas a los cálculos.
-- Uso: Widget de Ventas en página de Inicio (/Inicio.aspx).
CREATE PROCEDURE Widget_TotalVentas_UltimoDia AS
BEGIN
    SELECT 
        FORMAT(COALESCE(SUM(Ventas.PrecioTotal_Vt), 0), 'C', 'es-AR') as [Total], 
        GETDATE() as [Fecha]
    FROM Ventas 
    WHERE 
        Ventas.Fecha_Vt >= DATEADD(hour, -27, GETDATE()) 
        AND 
        Ventas.Fecha_Vt <= DATEADD(hour, -3, GETDATE())
END



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Devuelve el total de las ventas de los últimos siete días.
-- Observaciones: Dado que el servidor usa la hora de Greenwich y no es posible cambiarlo, les resté 3 horas a los cálculos.
-- Uso: Widget de Ventas en página de Inicio (/Inicio.aspx).
CREATE PROCEDURE Widget_TotalVentas_UltimaSemana AS
BEGIN
    SELECT 
        FORMAT(COALESCE(SUM(Ventas.PrecioTotal_Vt), 0), 'C', 'es-AR') as [Total], 
        GETDATE() as [Fecha]
    FROM Ventas 
    WHERE 
        Ventas.Fecha_Vt >= DATEADD(hour, -171, GETDATE()) 
        AND 
        Ventas.Fecha_Vt <= DATEADD(hour, -3, GETDATE())
END



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Devuelve la cantidad de productos que cuentan con stock menor a cinco.
-- Uso: Widget de Productos en página de Inicio (/Inicio.aspx).
CREATE PROCEDURE Widget_ContarProductosConBajoStock
AS
BEGIN
    -- Contar productos con stock menor a 5
    SELECT 
        COUNT(CodProducto_Prod) as [Cantidad]
        FROM Productos 
        WHERE 
            Stock_Prod <= 5 
            AND 
            Stock_Prod > 0
END



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Devuelve la cantidad de productos que cuentan con stock cero.
-- Uso: Widget de Productos en página de Inicio (/Inicio.aspx).
CREATE PROCEDURE Widget_ContarProductosSinStock AS
BEGIN
    -- Contar productos sin stock
    SELECT 
        COUNT(CodProducto_Prod) as [Cantidad]
        FROM Productos 
        WHERE 
            Stock_Prod = 0
END



--- PROCEDIMIENTO ALMACENADO
-- Descripción: Crea un registro en la tabla Ventas, y devuelve una tabla con el ID del nuevo registro y la cantidad de filas modificadas. Esta última se usa para verificar en el código que sí se haya podido ejecutar el INSERT.
-- Uso: Ventas/Crear.aspx.
CREATE PROCEDURE IniciarVenta
	@DNI char(12),
	@MEDIO varchar(50),
	@FECHA datetime,
	@TOTAL money
    AS
    BEGIN
        -- Declararmos variables
        DECLARE @RESULTADO TABLE (ID int, AFFECTEDROWS int)
        DECLARE @VentaID int

        -- Insertamos lo de la venta.
        INSERT INTO Ventas (DNIEmpleado_Vt, TipoPago_Vt, Fecha_Vt, PrecioTotal_Vt)
        VALUES (@DNI, @MEDIO, @FECHA, @TOTAL);

        -- Obtenemos el ID asignado a esta venta.
        SET @VentaID = SCOPE_IDENTITY();

        -- Obtenemos el número de filas modificadas
        DECLARE @FilasModificadas int;
        SET @FilasModificadas = @@ROWCOUNT;

        -- Devolvemos
        INSERT INTO @RESULTADO (ID, AFFECTEDROWS)
        VALUES (@VentaID, @FilasModificadas);

        SELECT ID, AFFECTEDROWS FROM @RESULTADO;
    END
GO



