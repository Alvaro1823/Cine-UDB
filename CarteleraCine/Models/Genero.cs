using System;
using System.Collections.Generic;

namespace CarteleraCine.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
