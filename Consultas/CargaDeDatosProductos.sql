use Pets
go

Insert into Productos (CodProducto_Prod,CUITProveedor_Prod,CodTipoProducto_Prod,
Nombre_Prod,Marca_Prod,Descripcion_Prod,Stock_Prod,PrecioUnitario_Prod,Estado_Prod)

SELECT 'PROD1','0987654321','TP4','Alimento Húmedo','Whiskas',	'Alimento balanceado para gatos',	67,	2477,1 UNION
SELECT 'PROD2','0987654321','TP4','Alimento Seco',  'Whiskas',	'Alimento balanceado para gatos',	83,	4244,1 UNION
SELECT 'PROD3','0987654321','TP4','Castrado',	   'Whiskas',	'Alimento balanceado para gatos',	65,	2466,1 UNION
SELECT 'PROD4','1234567890','TP4','Esterilizado',   'Cat Chow',	'Alimento balanceado para gatos',	69,	3224,1 UNION
SELECT 'PROD5','1234567890','TP1','Adultos Control de Peso','Dog Chow','Alimento balanceado para perros',69,3017,1 UNION
SELECT 'PROD6','1234567890','TP2','Mini Indoor Adult','Royal Canin', 'Alimento balanceado para perros',1,4450,1 UNION
SELECT 'PROD7','1234567890','TP2','Mix Cachorros','Sabrositos',	'Alimento balanceado para perros',	46,3591,1  UNION
SELECT 'PROD8','4443332220','TP2','Standard Adulto-Carne','Delivery Rocco Tiernitos','Alimento balanceado para perros',48,2175,1 UNION
SELECT 'PROD9','5556667778','TP3','Cachorro','Hills Science Diet','Alimento balanceado para perros',7,4191,1	  UNION
SELECT 'PROD10','7778889992','TP3','Adulto 1-6 años','Hills Science Diet','Alimento balanceado para perros',6,4623,1 UNION
SELECT 'PROD11','1234567890','TP1','Premium Performance','Eukanuba','Alimento balanceado para perros',88,4734,1 UNION
SELECT 'PROD12','9998887776','TP3','Adult Large Breed','Eukanuba','Alimento balanceado para perros',3,4849,1	  UNION
SELECT 'PROD13','9998887776','TP1','Puppy Small Breed','Eukanuba','Alimento balanceado para perros',55,4538,1  
GO