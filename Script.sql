CREATE DATABASE IF NOT EXISTS inventory_control;


CREATE TABLE IF NOT EXISTS Categories (
	Id INT AUTO_INCREMENT PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Description VARCHAR(255),
	IsActive BOOLEAN NOT NULL DEFAULT TRUE
);


CREATE TABLE IF NOT EXISTS Products (
	Id INT AUTO_INCREMENT PRIMARY KEY,
	Code VARCHAR(50) NOT NULL UNIQUE,
	Name VARCHAR(100) NOT NULL,
	Description VARCHAR(255),
	Price DECIMAL(18,2) NOT NULL,
	IsActive BOOLEAN NOT NULL DEFAULT TRUE,
	CategoryId INT NOT NULL,
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

CREATE TABLE IF NOT EXISTS Movements(
	Id INT AUTO_INCREMENT PRIMARY KEY,
	ProductId INT NOT NULL,
	MovementType INT NOT NULL, -- 1 = Entrada, 2 = Salida
	Quantity INT NOT NULL,
	MovementDate DATETIME NOT NULL,
	Remarks VARCHAR(255),
	FOREIGN KEY (ProductId) REFERENCES Products(Id)
);



INSERT INTO Categories (Name, Description, IsActive) VALUES 
('Electrónica', 'Dispositivos y gadgets tecnológicos', 1),
('Almacenamiento', 'Suministros de empaque y oficina para almacén', 1);

INSERT INTO Products (Code, Name, Description, Price, IsActive, CategoryId) VALUES 
('PROD-001', 'Mouse Logitech', 'Mouse ergonómico inalámbrico', 450.00, 1, 1),
('PROD-002', 'Cajas de Cartón', 'Cajas de cartón corrugado de alta resistencia', 25.50, 1, 2);


INSERT INTO Movements (ProductId, MovementType, Quantity, MovementDate, Remarks) VALUES 
(1, 1, 50, NOW(), 'Compra de productos');
