# FlightManagerMVC
### Flight Manager, distance and fuel calculator

Para ejecutar la aplicación web, seguir lo siguientes pasos:
	
 - Lo primero que demos hacer es descargar el proyecto y abrirlo con Visual Studio 2015 o superior.

 - Lo siguiente que debemos hacer es crear la base de datos y los objectos que nos van a hacer falta.
	 - Crear una nueva base de datos llamada ***dbFlightManager*** o utilizar una ya existente, siempre que modifiquemos la `connectionStrings` del `web.config`
	 - Crear la tabla ***Flight*** lanzando el siguiente script:
```
USE [dbFlightManager]
GO

IF EXISTS(SELECT * FROM [dbo].[Flight])
	DROP TABLE [dbo].[Flight]

CREATE TABLE [dbo].[Flight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Airline] [varchar](255) NOT NULL,
	[SourceAirportID] [varchar](5) NOT NULL,
	[SourceAirportName] [varchar](255) NOT NULL,
	[DestinationAirportID] [varchar](5) NOT NULL,
	[DestinationAirportName] [varchar](255) NOT NULL,
	[FuelNeeded] [decimal](18, 0) NULL,
	[Stops] [int] NULL,
	[Distance] [decimal](18, 0) NULL,
	[Active] [bit] NOT NULL,
	[LastModifiedUser] [varchar](255) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK__Flight__3214EC0707F6335A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
```

 - Una vez creada la tabla, nos aseguramos de que la `connectionStrings` del `web.config` del proyecto **ApiServices** esta bien configurada.
 
 - Desde Visual Studio nos vamos al proyecto **Presentation.Mvc**, lo marcamos como proyecto de inicio y ejecutamos la aplicación (F5).


