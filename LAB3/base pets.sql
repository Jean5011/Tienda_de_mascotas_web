use master
go

create database Pets
go

use Pets
go

/**** TABLAS ****/
---- TABLAS CREADAS POR JEAN ESQUEN ----
Create table Animales
(

Pk_CodAnimales_An varchar(10) not null,

nombre_An char(20) not null,

NombreDeRaza_An char(20) null,

estado_An bit not null default 1,

Constraint PK_Animales primary key (Pk_CodAnimales_An)
)
go

create TABLE TipoDeProductos
(
PK_CodTipoProducto_TP varchar(10) NOT NULL,
CodAnimales_Tp varchar(10) NOT NULL,
TipoDeProducto_Tp varchar(15) NOT NULL CHECK(TipoDeProducto_Tp='Comida' OR TipoDeProducto_Tp='Accesorios' OR TipoDeProducto_Tp='Ropa' OR TipoDeProducto_Tp='Higiene' OR TipoDeProducto_Tp='Salud'),
Descripcion_TP text NOT NULL,
estado_Tp bit not null default 1,
Constraint PK_TipoDeProductos primary key (PK_CodTipoProducto_TP),
constraint FK_TipoDeProductosxAnimales foreign key (CodAnimales_Tp)
references Animales(Pk_CodAnimales_An)
)
go

---- TABLAS CREADAS POR JAVIER ANDRÉS TORALES ----
CREATE TABLE Proveedores
(
CUIT_Prov varchar(15) NOT NULL,
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

---- TABLAS CREADAS POR MÁXIMO CANEDO ----
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

---- TABLAS CREADAS POR EZEQUIEL ALEJANDRO MARTíNEZ ----
Create Table Productos
(
CodProducto_Prod varchar(10) not null,
CUITProveedor_Prod varchar(15) not null,
CodTipoProducto_Prod varchar(10) not null,
Nombre_Prod varchar(50) not null,
Marca_Prod varchar(50) not null,
Descripcion_Prod varchar(50) not null,
Stock_Prod int not null,
Imagen_Prod varchar(100) null,
PrecioUnitario_Prod money not null,
Estado_Prod bit default 1,
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

---- TABLAS CREADAS POR MARÍA OLIVIA HANCZYC  ----
CREATE TABLE DetalleDeVenta
(
CodVenta_Dv int NOT NULL,
CodProducto_Dv varchar(10) NOT NULL,
CUITProveedor_Dv varchar(15) NOT NULL,
Cantidad_Dv int NOT NULL,
PrecioUnitario_Dv money NOT NULL,
PrecioTotal_Dv money NOT NULL,
Estado_Dv bit default 1,
CONSTRAINT PK_DetalleDeVenta PRIMARY KEY (CodVenta_Dv, CodProducto_Dv, CUITProveedor_Dv),
CONSTRAINT FK_DetalleDeVenta_Ventas FOREIGN KEY (CodVenta_Dv) REFERENCES Ventas (CodVenta_Vt),
CONSTRAINT FK_DetalleDeVenta_Productos FOREIGN KEY (CodProducto_Dv, CUITProveedor_Dv) REFERENCES Productos (CodProducto_Prod, CUITProveedor_Prod)
)
GO

/**** PROCEDIMIENTOS ALMACENADOS ****/
----------------------------------------------------------------------------------------------------------------------------------------------
/*-- PROCEDIMIENTOS CREADOS POR JEAN ESQUEN ---*/
--Agregar--
CREATE procedure SP_IngresarTipoDeProductos
(
@PK_CodTipoProducto_TP varchar(10),
@CodAnimales_Tp varchar(10) ,
@TipoDeProducto_Tp varchar(15),
@Descripcion_TP text
)
as
if @PK_CodTipoProducto_TP =(select PK_CodTipoProducto_TP from TipoDeProductos)
begin 
print 'Existe Tipo de productos'
end
else 
begin
insert into TipoDeProductos(PK_CodTipoProducto_TP,CodAnimales_Tp,TipoDeProducto_Tp,Descripcion_TP)
select @PK_CodTipoProducto_TP,@CodAnimales_Tp,@TipoDeProducto_Tp,@Descripcion_TP
end
go

CREATE procedure SP_IngresarAnimal
(
@PK_CodAnimales_An varchar(10),
@nombre_An char(20),
@NombreDeRaza_An char(20)
)
as 
if @PK_CodAnimales_An = (select PK_CodAnimales_An from Animales) and upper(@NombreDeRaza_An) = (select NombreDeRaza_An from Animales) AND upper(@nombre_An) = (select nombre_An from Animales)
begin
 return null
end
else
begin
insert into Animales(PK_CodAnimales_An,nombre_An,NombreDeRaza_An)
select @PK_CodAnimales_An,@nombre_An,upper(@NombreDeRaza_An)
end 
go

--Baja--
CREATE procedure SP_EliminarAnimal
(
@PK_CodAnimales_An varchar(10)
)
as 
update Animales set estado_An = 0 where  PK_CodAnimales_An=@PK_CodAnimales_An
go

CREATE procedure SP_EliminarTipoDeProductos
(
@PK_CodTipoProducto_TP varchar(10)
)
as 
update TipoDeProductos set estado_Tp = 0 where  PK_CodTipoProducto_TP=@PK_CodTipoProducto_TP 
go

--Alta--
create procedure SP_AltaAnimal
(
@PK_CodAnimales_An varchar(10)
)
as 
update Animales set estado_An = 1 where  PK_CodAnimales_An=@PK_CodAnimales_An
go

create procedure SP_AltaTipoDeProductos
(
@PK_CodTipoProducto_TP varchar(10)
)
as 
update TipoDeProductos set estado_Tp = 1 where  PK_CodTipoProducto_TP=@PK_CodTipoProducto_TP 
go

--Actualizar--
create procedure SP_ActualizarTipoProducto
(
@PK_CodTipoProducto_TP varchar(10),
@CodAnimales_Tp varchar(10),
@TipoDeProducto_Tp varchar(15),
@Descripcion_TP text
)
as 
update TipoDeProductos set  CodAnimales_Tp = @CodAnimales_Tp,TipoDeProducto_Tp = @TipoDeProducto_Tp,Descripcion_TP = @Descripcion_TP 
where  PK_CodTipoProducto_TP = @PK_CodTipoProducto_TP
go

create procedure SP_ActualizarAnimales
(
@PK_CodAnimales_An varchar(10),
@nombre_An char(20),
@NombreDeRaza_An char(20)
)
AS
update Animales set nombre_An=@nombre_An,NombreDeRaza_An = upper(@NombreDeRaza_An) where PK_CodAnimales_An=@PK_CodAnimales_An
go

----------------------------------------------------------------------------------------------------------------------------------------------
/*-- PROCEDIMIENTOS CREADOS POR MÁXIMO CANEDO ---*/
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
GO

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
GO

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
GO

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
GO

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
GO

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

----------------------------------------------------------------------------------------------------------------------------------------------
/*-- PROCEDIMIENTOS CREADOS POR JAVIER ANDRÉS TORALES ---*/
CREATE PROCEDURE SP_Proveedor_Crear
@CUIT varchar(15),
@RazonSocial varchar(50),
@NombreContacto varchar(30),
@CorreoElectronico varchar(75),
@Telefono varchar(20),
@Direccion varchar(30),
@Provincia varchar(30),
@Localidad varchar(30),
@Pais varchar(20),
@CodigoPostal varchar(10)
AS
BEGIN
INSERT INTO Proveedores(CUIT_Prov,RazonSocial_Prov,NombreDeContacto_Prov,CorreoElectronico_Prov,Telefono_Prov,Direccion_Prov,Provincia_Prov,Localidad_Prov,Pais_Prov,CodigoPostal_Prov)
SELECT @CUIT,@RazonSocial,@NombreContacto,@CorreoElectronico,@Telefono,@Direccion,@Provincia,@Localidad,@Pais,@CodigoPostal
END
GO

CREATE PROCEDURE SP_Proveedores_ActualizarEstado
@CUIT varchar(15),
@Estado bit
AS
BEGIN
UPDATE Proveedores
SET Estado_Prov=@Estado
where CUIT_Prov=@CUIT
END
GO

CREATE PROCEDURE SP_Proveedores_Actualizar
@CUIT varchar(15),
@RazonSocial varchar(50),
@NombreContacto varchar(30),
@CorreoElectronico varchar(75),
@Telefono varchar(20),
@Direccion varchar(30),
@Provincia varchar(30),
@Localidad varchar(30),
@Pais varchar(20),
@CodigoPostal varchar(10)
AS
BEGIN
UPDATE Proveedores
SET 
RazonSocial_Prov=@RazonSocial,
NombreDeContacto_Prov=@NombreContacto,
CorreoElectronico_Prov=@CorreoElectronico,
Telefono_Prov=@Telefono,
Direccion_Prov=@Direccion,
Provincia_Prov=@Provincia,
Localidad_Prov=@Localidad,
Pais_Prov=@Pais,
CodigoPostal_Prov=@CodigoPostal
WHERE CUIT_Prov=@CUIT
END
GO

----------------------------------------------------------------------------------------------------------------------------------------------
/*-- PROCEDIMIENTOS CREADOS POR MARÍA OLIVIA HANCZYC  ---*/
CREATE PROCEDURE SP_DetalleDeVenta_Agregar
@CodigoVenta int,
@CodigoProducto varchar(10),
@CUITProveedor varchar(15),
@Cantidad int
AS
	BEGIN
		DECLARE @PrecioUnitario money

		--Obtener el precio unitario de la tabla Productos:
		SELECT @PrecioUnitario = PrecioUnitario_Prod
		FROM Productos
		WHERE CodProducto_Prod = @CodigoProducto

		--Insertar el registro en la tabla DetalleDeVenta:
		INSERT INTO DetalleDeVenta (CodVenta_Dv, CodProducto_Dv, CUITProveedor_Dv, Cantidad_Dv, PrecioUnitario_Dv, PrecioTotal_Dv, Estado_Dv)
		SELECT @CodigoVenta, @CodigoProducto, @CUITProveedor, @Cantidad, @PrecioUnitario, (@Cantidad * @PrecioUnitario), 1
	END
GO 

--NOTA: este procedimiento (SP_DetalleDeVentas_DarDeBaja) fue realizado también por Ezequiel Alejandro Martínez:
CREATE PROCEDURE SP_DetalleDeVentas_DarDeBaja
@CodigoVenta int
AS
BEGIN
UPDATE DetalleDeVenta 
SET Estado_Dv = 0
WHERE CodVenta_Dv = @CodigoVenta
END
GO

--Este procedimiento disminuye la cantidad en la tabla DetalleDeVenta, y aumenta el stock en la tabla Productos:
CREATE PROCEDURE SP_DetalleDeVenta_disminuirCantidadVendida
@CodigoVenta int, @CodigoProducto varchar(10), @CUITProveedor varchar(15)
AS
DECLARE @Stock int, @Resultado bit --(Resultado que devuelve el procedimiento).
SELECT @Stock = Stock_Prod FROM Productos WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor
IF(@Stock > 0) --Si el stock es mayor a cero, entonces:
	BEGIN
		UPDATE DetalleDeVenta
		SET Cantidad_Dv = Cantidad_Dv - 1 --Se le resta una unidad a la cantidad.
		WHERE CodVenta_Dv = @CodigoVenta AND CodProducto_Dv = @CodigoProducto AND CUITProveedor_Dv = @CUITProveedor

		UPDATE Productos
		SET Stock_Prod = Stock_Prod + 1 --Se actualiza el stock en la tabla Productos, sumándole una unidad (ya que se vende menos cantidad).
		WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor

		SET @Resultado = 1
		SELECT @Resultado AS RESULTADO
	END
ELSE --En cambio, si el stock es menor o igual a cero:
	BEGIN
		SET @Resultado = 0
		SELECT @Resultado AS RESULTADO
	END
GO

--Este procedimiento aumenta la cantidad en la tabla DetalleDeVenta, y disminuye el stock en la tabla Productos:
CREATE PROCEDURE SP_DetalleDeVenta_aumentarCantidadVendida
@CodigoVenta int, @CodigoProducto varchar(10), @CUITProveedor varchar(15)
AS
DECLARE @Stock int, @Resultado bit --(Resultado que devuelve el procedimiento).
SELECT @Stock = Stock_Prod FROM Productos WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor
IF(@Stock > 0) --Si el stock es mayor a cero, entonces:
	BEGIN
		UPDATE DetalleDeVenta
		SET Cantidad_Dv = Cantidad_Dv + 1 --Se le suma una unidad a la cantidad.
		WHERE CodVenta_Dv = @CodigoVenta AND CodProducto_Dv = @CodigoProducto AND CUITProveedor_Dv = @CUITProveedor

		UPDATE Productos
		SET Stock_Prod = Stock_Prod - 1 --Se actualiza el stock en la tabla Productos, restándole una unidad (ya que se vende más cantidad).
		WHERE CodProducto_Prod = @CodigoProducto AND CUITProveedor_Prod = @CUITProveedor

		SET @Resultado = 1
		SELECT @Resultado AS RESULTADO
	END
ELSE --En cambio, si el stock es menor o igual a cero:
	BEGIN
		SET @Resultado = 0
		SELECT @Resultado AS RESULTADO
	END
GO

----------------------------------------------------------------------------------------------------------------------------------------------
/*-- PROCEDIMIENTOS CREADOS POR EZEQUIEL ALEJANDRO MARTíNEZ  ---*/
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

CREATE PROCEDURE SP_Productos_Crear
@Codigo varchar(10),
@CUIT varchar(15),
@Tipo varchar(10),
@Nombre varchar(50),
@Marca varchar(50),
@Desc varchar(50),
@Stock int,
@Precio money,
@Estado bit
AS
BEGIN
INSERT INTO Productos(CodProducto_Prod,CUITProveedor_Prod,CodTipoProducto_Prod,Nombre_Prod,Marca_Prod,Descripcion_Prod,Stock_Prod,PrecioUnitario_Prod,Estado_Prod)
SELECT @Codigo,@CUIT,@Tipo,@Nombre,@Marca,@Desc,@Stock,@Precio,@Estado
END
GO

CREATE PROCEDURE SP_Productos_ActualizarEstado
@Codigo varchar(10),
@Estado bit
AS
BEGIN
UPDATE Productos
SET Estado_Prod=@Estado
where CodProducto_Prod=@Codigo
END
GO

CREATE PROCEDURE SP_Productos_ActualizarPrecio
@Codigo varchar(10),
@Precio money
AS
BEGIN
UPDATE Productos
SET PrecioUnitario_Prod=@Precio
where CodProducto_Prod=@Codigo
END
GO

CREATE PROCEDURE SP_Productos_ActualizarStock
@Codigo varchar(10),
@Stock int
AS
BEGIN
UPDATE Productos
SET Stock_Prod=@Stock
where CodProducto_Prod=@Codigo
END
GO

CREATE PROCEDURE SP_Productos_Actualizar
@Codigo varchar(10),
@CUIT varchar(15),
@Tipo varchar(10),
@Nombre varchar(50),
@Marca varchar(50),
@Desc varchar(50),
@Stock int,
@Precio money,
@Estado bit
AS
BEGIN
UPDATE Productos
SET 
CUITProveedor_Prod=@CUIT,
CodTipoProducto_Prod=@Tipo,
Nombre_Prod=@Nombre,
Marca_Prod=@Marca,
Descripcion_Prod=@Desc,
Stock_Prod=@Stock,
PrecioUnitario_Prod=@Precio,
Estado_Prod=@Estado
WHERE CodProducto_Prod=@Codigo
END
GO

/**** Triggers ****/
/*--- TRIGGER CREADO POR MARÍA OLIVIA HANCZYC  ---*/
CREATE TRIGGER TR_actualizarStockProductos
ON DetalleDeVenta AFTER INSERT AS
	BEGIN 
		SET NOCOUNT ON 
		UPDATE Productos
		SET Stock_Prod = Stock_Prod - (SELECT Cantidad_Dv FROM INSERTED)
		WHERE CodProducto_Prod = (SELECT CodProducto_Dv FROM INSERTED) AND
		CUITProveedor_Prod = (SELECT CUITProveedor_Dv FROM INSERTED) and Stock_Prod > 0 --Stock_Prod <> 0
	END
GO

/*-- TRIGGER CREADO POR EZEQUIEL ALEJANDRO MARTíNEZ  ---*/
CREATE TRIGGER TR_Productos_PrevenirEliminar
ON Productos
INSTEAD OF DELETE
AS
BEGIN
    PRINT('No está permitido eliminar registros de la tabla "Productos" mediante el uso de "Delete".');
    ROLLBACK;
END;
go

/*--- TRIGGER CREADO POR JAVIER ANDRÉS TORALES ---*/
CREATE TRIGGER TR_Proveedores_PrevenirEliminar
ON Proveedores
INSTEAD OF DELETE
AS
BEGIN
    PRINT('No está permitido eliminar registros de la tabla "Proveedores" mediante el uso de "Delete".');
    ROLLBACK;
END;
go

---- Integrantes ----
select  upper('Javier Andres Torales'), upper('María Olivia Hanczyc '),upper('Ezequiel Alejandro Martinez'),upper('Máximo Canedo'),upper('jean esquen')