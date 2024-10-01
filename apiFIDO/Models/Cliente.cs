using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public int? IdRaza { get; set; }

    public decimal? PesoPerro { get; set; }

    public virtual ICollection<CanjePremio> CanjePremios { get; set; } = new List<CanjePremio>();

    public virtual RazaPerro? IdRazaNavigation { get; set; }
}
