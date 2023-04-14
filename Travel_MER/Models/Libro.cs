using System;
using System.Collections.Generic;

namespace Travel_MER.Models;

public partial class Libro
{
    public long Isbn { get; set; }

    public int? EditorialId { get; set; }

    public string? Titulo { get; set; }

    public string? Sinopsis { get; set; }

    public string? NPaginas { get; set; }

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
