using System;
using System.Collections.Generic;

namespace bibliotecaAPI.Entities;

public partial class Editorial
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FecCreacion { get; set; }

    public ulong? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
