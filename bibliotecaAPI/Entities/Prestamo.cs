using System;
using System.Collections.Generic;

namespace bibliotecaAPI.Entities;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? Socio { get; set; }

    public int? Libro { get; set; }

    public DateTime? FecPrestamo { get; set; }

    public DateTime? FecDevolucion { get; set; }

    public decimal? TotalPrestamo { get; set; }

    public ulong? Estado { get; set; }

    public virtual Libro? LibroNavigation { get; set; }

    public virtual Socio? SocioNavigation { get; set; }
}
