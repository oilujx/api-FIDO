using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class Codigo
{
    public int IdCodigo { get; set; }

    public string Codigo1 { get; set; } = null!;

    public virtual ICollection<CodigoPremio> CodigoPremios { get; set; } = new List<CodigoPremio>();
}
