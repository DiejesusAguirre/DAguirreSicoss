CREATE DATABASE DAguirreSicoss

--------------------TABLAS-------------------

CREATE TABLE Usuario(IdUsuario INT PRIMARY KEY IDENTITY(1,1),
Usuario VARCHAR(50)UNIQUE NOT NULL, Password VARCHAR(50) NOT NULL)

CREATE TABLE Operaciones(IdOperacion INT PRIMARY KEY IDENTITY(1,1),
Numero INT, Resultado INT, FechaHora DATETIME, IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario))

-------------------------------STORED PROCEDURE---------------------

CREATE PROCEDURE GetByUsuario
@UserName VARCHAR(50),
@Password VARCHAR(50)
AS
SELECT Usuario, Password
FROM Usuario
WHERE Usuario=@UserName AND Password=@Password

CREATE PROCEDURE AddUsuario
@UserName VARCHAR(50),
@Password VARCHAR(50)
AS
INSERT INTO Usuario(Usuario,Password)
VALUES (@UserName,@Password)

CREATE PROCEDURE GetByIdUsuario
@IdUsuario INT
AS
SELECT IdOperacion, Numero, Resultado, FechaHora, IdUsuario
FROM Operaciones
WHERE IdUsuario=@IdUsuario

CREATE PROCEDURE AddOperacion
@Numero INT,
@Resultado INT,
@IdUsuario INT
AS
INSERT INTO Operaciones(Numero,Resultado,FechaHora,IdUsuario)
VALUES (@Numero,@Resultado,GETDATE(),@IdUsuario)

CREATE PROCEDURE GetByNumero
@Numero INT
AS
SELECT Numero, Resultado
FROM Operaciones
WHERE Numero=@Numero
UPDATE Operaciones
SET FechaHora=GETDATE()
WHERE Numero=@Numero

SELECT * FROM Usuario

CREATE PROCEDURE ExisteNumero
@Numero INT
AS
SELECT COUNT(*) AS 'Existe'
FROM Operaciones
WHERE Numero = @Numero