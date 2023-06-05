using System;
using System.Collections.Generic;

namespace CarteleraCine.Models;

public partial class Pelicula
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdGenero { get; set; }

    public int IdDirector { get; set; }

    public int? Puntos { get; set; }

    public string Imagen { get; set; } = null!;

    public virtual Director IdDirectorNavigation { get; set; } = null!;

    public virtual Genero IdGeneroNavigation { get; set; } = null!;
}
