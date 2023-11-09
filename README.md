# Prueba Técnica Farmacorp

En este repositorio se encuentra la prueba técnica desarrollada para Farmacorp.
A continuación se detallan las instrucciones para correr el proyecto




# Getting Started

Para utilizar este repositorio por favor descargelo o clonelo a su PC

 ## Usando GitHub
 
Para comenzar con este repositorio, necesita obtener una copia localmente. Tienes tres opciones: bifurcar, clonar o descargar. La mayoría de las veces, probablemente sólo quieras descargar.


## Crear Base de Datos SQL Server

Este proyecto utiliza una base de datos SQL Server, pero no está programada para crear una desde cero. Por favor, crea una usando SQL Management Studio y llamala como más te guste

## Actualizar Connection String

En la carpeta FarmacorpPos.API  se pueden encontrar los appsettings.json. Por favor, una vez que hayas creado la base de datos actualiza la cadena de conexión. 

## Ejecutando migraciones
En Visual Studio, abra la consola del administrador de paquetes y ejecute `add-migration InitialMigrationName`

Después de crear la carpeta de migraciones, ejecute el siguiente comando `update-database`

**Importante, debe seleccionar FarmacorpPOS.Infrastructure en la consola de Paquect Manager como proyecto predeterminado al ejecutar estos comandos**

**El proyecto está configurado para que se inicialice con datos iniciales**
## Ejecutar el proyecto

El archivo de la solución se encuentra dentro de la carpeta FarmacorpPos.Api y debe seleccionarse como proyecto de inicio.

Para hacerlo, haga clic derecho en el proyecto FarmacorpPos.Api y seleccione la opción **Configurar como proyecto de inicio predeterminado**

# EXPLICACIÓN DE LOS REQUERIMIENTOS

Todos los requerimientos fueron desarrollados en su totalidad. Se tiene 4 Endpoints los cuales hacen lo siguiente

## 1. Registar un nuevo producto POST /products
	Para registrar un nuevo producto se requiere mandar la siguiente estructura:
	```
	{
	  "productName": "string",
	  "expirationDate": "2023-11-08T20:42:02.223Z",
	  "observations": "string",
	  "productTypeId": 0,
	  "cost": 0,
	  "stock": 0
	}
	```
	Automaticamente cuando un nuevo producto se registra, registra los registros necesarios en el producto Express y ERP
	El producto puede ser creado sin codigo de barra y sin categorias
## 2. Registrar un nuevo Código de Barra POST /barcode
	Para este requerimiento solamente se requiere mandar el Id del Producto y el estado del código de barras
	```
	{
	  "productId": 0,
	  "active": true
	}	
	```
	Automaticamente realizará las operaciones descritas en el requerimiento

## 3. Registar una nueva Venta POST /sales
    Para este requerimiento solamente se requiere mandar el Id del Producto, Nombre del Cliente y la cantidad vendida
	```
	{
	  "clientFullName": "string",
	  "quantity": 0,
	  "productId": 0
	}	
	```
	Automaticamente realizará las operaciones descritas en el requerimiento, solamente acepta un producto por venta

## 4. Asignación de Categoria a un producto POST /products/productId/categories
	
	Para este requerimiento se trabajo en el metodo post del recurso producto donde
	el product Id se lo manda desde la ruta y el ID de la catogoria a asignar se lo manda desde el body
	```
    {
	  "categoryId": 0
	}
	
	```
	Una vez insertado, la api trabajará según los requerimientos

## 5. Estrategia GANAMAX
	Para este requerimiento se trabajo en la implementación y abstracción para soportar ambos casos, cuando utilicemos la estrategia GANAMAX o no
	Parte de la solución se encuentra en los appsettings de la siguiente forma
	```
	"SaleStrategyConfig": {
		"SaleMode":  "NewSale"
		//"SaleMode": "BaseSale"
	}
	```
	Para cambiar el tipo de estrategia solamente basta con cambiar el valor de BaseSale para los casos normales a NewSale para la estrategia GANAMAX
	Solamente con ese cambio la aplicación se comportará como debe según el tipo de estrategia
# Registro de logs
Para registrar los logs se utilizó Serilog, los logs iran saliendo en la consola. Además que se escribiran en el archivo indicado en los appsettings