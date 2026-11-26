-- =====================================================
-- Sistema de Ventas - Programación 5
-- Prototipo de base de datos en MySQL
-- =====================================================

CREATE DATABASE IF NOT EXISTS sistema_ventas
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE sistema_ventas;

-- -----------------------------------------------------
-- Categorías
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS categorias (
    id          INT AUTO_INCREMENT PRIMARY KEY,
    nombre      VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(255)
);

-- -----------------------------------------------------
-- Productos
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS productos (
    codigo       VARCHAR(20)   PRIMARY KEY,
    nombre       VARCHAR(100)  NOT NULL,
    descripcion  VARCHAR(255),
    precio       DECIMAL(10,2) NOT NULL CHECK (precio >= 0),
    stock        INT           NOT NULL DEFAULT 0 CHECK (stock >= 0),
    categoria_id INT,
    CONSTRAINT fk_productos_categoria
        FOREIGN KEY (categoria_id) REFERENCES categorias(id)
        ON DELETE SET NULL
);

-- -----------------------------------------------------
-- Clientes
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS clientes (
    id       INT AUTO_INCREMENT PRIMARY KEY,
    nombre   VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo   VARCHAR(100) UNIQUE
);

-- -----------------------------------------------------
-- Ventas (cabecera)
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS ventas (
    id         INT AUTO_INCREMENT PRIMARY KEY,
    fecha      DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cliente_id INT           NOT NULL,
    total      DECIMAL(12,2) NOT NULL DEFAULT 0,
    CONSTRAINT fk_ventas_cliente
        FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

-- -----------------------------------------------------
-- Detalle de venta
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS detalle_venta (
    id              INT AUTO_INCREMENT PRIMARY KEY,
    venta_id        INT           NOT NULL,
    producto_codigo VARCHAR(20)   NOT NULL,
    cantidad        INT           NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal        DECIMAL(12,2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED,
    CONSTRAINT fk_detalle_venta
        FOREIGN KEY (venta_id) REFERENCES ventas(id)
        ON DELETE CASCADE,
    CONSTRAINT fk_detalle_producto
        FOREIGN KEY (producto_codigo) REFERENCES productos(codigo)
);

-- =====================================================
-- Datos de prueba (ejecutar UNA sola vez, después de crear las tablas)
-- =====================================================
INSERT INTO categorias (nombre, descripcion) VALUES
    ('Bebidas', 'Refrescos, jugos y agua'),
    ('Snacks', 'Golosinas y bocadillos'),
    ('Limpieza', 'Productos de limpieza');

INSERT INTO productos (codigo, nombre, descripcion, precio, stock, categoria_id) VALUES
    ('P001', 'Agua 600ml', 'Botella de agua', 500.00, 50, 1),
    ('P002', 'Refresco 2L', 'Gaseosa sabor cola', 1500.00, 30, 1),
    ('P003', 'Papas fritas', 'Bolsa mediana', 900.00, 40, 2),
    ('P004', 'Jabón líquido', 'Botella 1L', 2200.00, 15, 3);

INSERT INTO clientes (nombre, telefono, correo) VALUES
    ('Cliente Contado', NULL, NULL),
    ('María Rodríguez', '8888-1111', 'maria@correo.com'),
    ('Carlos Jiménez', '8888-2222', 'carlos@correo.com');