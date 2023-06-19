USE Pets
GO

INSERT INTO DetalleDeVenta (CodVenta_Dv, CodProducto_Dv, CUITProveedor_Dv, Cantidad_Dv, PrecioUnitario_Dv, PrecioTotal_Dv, Estado_Dv)
SELECT 1, 'PROD1', '0987654321', 2, 2100, 4200, 1 UNION
SELECT 2, 'PROD10', '7778889992', 4, 1625, 6500, 1 UNION
SELECT 3, 'PROD11', '1234567890', 3, 1200, 3600, 1 UNION
SELECT 4, 'PROD12', '9998887776', 1, 1520, 1520, 1 UNION
SELECT 5, 'PROD13', '9998887776', 5, 480, 2400, 1 UNION
SELECT 6, 'PROD2', '0987654321', 4, 1850, 7400, 1 UNION
SELECT 7, 'PROD3', '0987654321', 2, 1200, 2400, 1 UNION
SELECT 8, 'PROD4', '1234567890', 5, 2040, 10200, 1 UNION
SELECT 9, 'PROD5', '1234567890', 3, 3500, 10500, 1 UNION
SELECT 10, 'PROD6', '1234567890', 1, 5000, 5000, 1 UNION
SELECT 11, 'PROD7', '1234567890', 5, 2480, 12400, 1 UNION
SELECT 12, 'PROD8', '4443332220', 4, 875, 3500, 1 UNION
SELECT 13, 'PROD9', '5556667778', 2, 3150, 6300, 1
GO