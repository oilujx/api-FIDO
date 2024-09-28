using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class Premio
{
    public int IdPremio { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<CodigoPremio> CodigoPremios { get; set; } = new List<CodigoPremio>();
}
