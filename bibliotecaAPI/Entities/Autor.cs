using System;
using System.Collections.Generic;

namespace bibliotecaAPI.Entities;

/// <summary>
/// 		
/// </summary>
public partial class Autor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? FecCreacion { get; set; }

    public ulong? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
