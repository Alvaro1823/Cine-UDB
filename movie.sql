

CREATE DATABASE cartelera_cine;
GO
USE cartelera_cine;

CREATE TABLE genero (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);
GO

-- Crear la tabla "director"
CREATE TABLE director (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE peliculas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    titulo VARCHAR(100) NOT NULL,
	descripcion VARCHAR(MAX) NOT NULL,
    id_genero INT NOT NULL,
    id_director INT NOT NULL,
    puntos INT null,
	imagen VARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_peliculas_genero FOREIGN KEY (id_genero) REFERENCES genero(id),
    CONSTRAINT FK_peliculas_director FOREIGN KEY (id_director) REFERENCES director(id)
);
GO



ALTER TABLE peliculas
Add constraint df_puntos default 0 for puntos



    