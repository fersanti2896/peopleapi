-- Crear la base de datos
CREATE DATABASE PeopleAPI;
GO

-- Usar la base de datos recién creada
USE PeopleAPI;
GO

-- Crear la tabla Peoples
CREATE TABLE Peoples (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(MAX),
    LastName VARCHAR(MAX),
    Age INT,
    Email VARCHAR(MAX),
    DateCreated DATETIME2(7)
);
GO

-- Insertar registros de ejemplo en la tabla Peoples
INSERT INTO Peoples (Name, LastName, Age, Email, DateCreated)
VALUES
    ('Fernando', 'Nicolás', 27, 'fersanti2896@gmail.com', '2024-05-03 08:30:00'),
    ('María', 'Dae', 25, 'maria@mail.com', '2024-05-03 10:45:00'),
    ('Marisol', 'Contreras', 40, 'marisol@mail.com', '2024-05-03 12:15:00'),
    ('Ana', 'Martínez', 35, 'ana@mail.com', '2024-05-03 14:20:00'),
    ('Luis', 'Nicolás', 33, 'luis@mail.com', '2024-05-02 16:00:00');
