using System;
using System.Collections.Generic;

namespace bibliotecaAPI.Entities;

public partial class Libro
{
    public int Isbn { get; set; }

    public string? Titulo { get; set; }

    public int Editorial { get; set; }

    public int? AnioEdicion { get; set; }

    public int Autor { get; set; }

    public decimal? PrecioPrestamo { get; set; }

    public DateTime? FecCreacion { get; set; }

    public ulong? Estado { get; set; }

    public virtual Autor AutorNavigation { get; set; } = null!;

    public virtual Editorial EditorialNavigation { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
