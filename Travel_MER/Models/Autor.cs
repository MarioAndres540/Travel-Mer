using System;
using System.Collections.Generic;

namespace Travel_MER.Models;

public partial class Autor
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public virtual ICollection<Libro> LibroIsbns { get; set; } = new List<Libro>();
}
