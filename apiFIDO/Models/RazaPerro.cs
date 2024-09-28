using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class RazaPerro
{
    public int IdRaza { get; set; }

    public string? Nombre { get; set; }

    public ulong? Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
