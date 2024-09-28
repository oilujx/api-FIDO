using System;
using System.Collections.Generic;

namespace bibliotecaAPI.Entities;

public partial class Socio
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateTime? FecCreacion { get; set; }

    public ulong? Estado { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
