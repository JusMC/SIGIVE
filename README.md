# Sistema De Gestión De Inventario Y Ventas

Proyecto final del curso **Programación 5**, impartido por el profesor Pablo Cordero Vega.

Sistema de ventas desarrollado en C# que permite gestionar productos, clientes y ventas, con cálculo automático de totales y control de stock.

## Integrantes

- Justin Méndez
- Sebastián Elizondo
- Emerson Arguello
- Isaac Rodriguez
- Andy Melendez
- Jeremy Chaves

## Tecnologías

- C# / .NET 8.0
- Visual Studio
- MySQL
- WinForms
- Git / GitHub
- ClosedXML (exportar a Excel) y QuestPDF (exportar a PDF, licencia Community)

## Cómo ejecutarlo

1. Ejecutar `database/schema.sql` en MySQL Workbench (crea la base `sistema_ventas` con datos de prueba).
2. Cambiar `Uid` y `Pwd` en `DAL/ConexionBD.cs` según su MySQL.
3. Abrir `SIGIVE.slnx` en Visual Studio y ejecutar.

## Estructura (arquitectura en capas)

| Carpeta        | Capa                   | Qué hace                                                    |
|----------------|------------------------|-------------------------------------------------------------|
| `Views/`       | Presentación (UI)      | Formularios WinForms; solo piden datos y muestran mensajes  |
| `Views/Controles/` | Presentación (UI)  | Controles propios (`GraficoBarras` para el dashboard)       |
| `BLL/`         | Negocio                | Validaciones, cálculos (LINQ) y reglas del sistema          |
| `DAL/`         | Datos                  | DAO con consultas SQL parametrizadas (ADO.NET + MySQL)      |
| `DAL/Interfaces/` | Datos (contratos)   | Interfaces de repositorio (`IProductoRepositorio`, etc.)    |
| `DTO/`         | Objetos de transferencia | Clases que llevan los datos entre capas                   |
| `Excepciones/` | —                      | Excepciones personalizadas (`ValidacionException`, etc.)    |
| `Utilidades/`  | —                      | `Logger` (errores en `errores.log`) y exportadores a Excel/PDF |

Flujo: `Views → BLL → IRepositorio (DAO) → MySQL`

### Patrones de diseño

- **DTO:** objetos que transportan datos entre capas (`DTO/`).
- **DAO:** una clase por tabla con el SQL (`ProductoDAO`, `ClienteDAO`, ...).
- **Repositorio:** cada DAO implementa una interfaz (`IProductoRepositorio`, ...). La BLL solo conoce la interfaz, no MySQL.
- **Factory:** `DAL/FabricaRepositorios.cs` es el único lugar que decide qué clase concreta se usa. Las BLL piden ahí su repositorio.

## Funcionalidades

- CRUD de productos (código, nombre, descripción, precio, stock)
- CRUD de clientes (ID, nombre, teléfono, correo electrónico)
- Registro de ventas con detalle de productos, cantidades y total
- Reportes (menú **Reportes**), filtrados por rango de fechas:
  - **Ventas:** filtro por cliente, detalle de la venta seleccionada y total
  - **Productos:** inventario, con filtro de stock bajo (filas en rojo) y valor del inventario
  - **Más vendidos:** unidades y dinero por producto
  - **Clientes:** compras y total gastado por cliente
  - **Dashboard:** total de ventas, total vendido, promedio por venta, productos con stock bajo y gráficos de ventas por día y top 5 productos
  - Exportación de cualquier tabla a **Excel (.xlsx)** y **PDF**
