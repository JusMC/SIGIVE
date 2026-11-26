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

## Cómo ejecutarlo

1. Ejecutar `database/schema.sql` en MySQL Workbench (crea la base `sistema_ventas` con datos de prueba).
2. Cambiar `Uid` y `Pwd` en `DAL/ConexionBD.cs` según su MySQL.
3. Abrir `SIGIVE.slnx` en Visual Studio y ejecutar.

## Estructura (arquitectura en capas)

| Carpeta        | Capa                   | Qué hace                                                    |
|----------------|------------------------|-------------------------------------------------------------|
| `Views/`       | Presentación (UI)      | Formularios WinForms; solo piden datos y muestran mensajes  |
| `BLL/`         | Negocio                | Validaciones, cálculos (LINQ) y reglas del sistema          |
| `DAL/`         | Datos                  | DAO con consultas SQL parametrizadas (ADO.NET + MySQL)      |
| `DTO/`         | Objetos de transferencia | Clases que llevan los datos entre capas                   |
| `Excepciones/` | —                      | Excepciones personalizadas (`ValidacionException`, etc.)    |
| `Utilidades/`  | —                      | `Logger`: guarda errores en `errores.log`                   |

Flujo: `Views → BLL → DAL → MySQL`

## Funcionalidades

- CRUD de productos (código, nombre, descripción, precio, stock)
- CRUD de clientes (ID, nombre, teléfono, correo electrónico)
- Registro de ventas con detalle de productos, cantidades y total
- Reportes de ventas, productos y clientes con exportación a PDF y Excel
