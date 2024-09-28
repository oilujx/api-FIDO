using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class CodigoPremio
{
    public int IdCodigoPremio { get; set; }

    public int? IdCodigo { get; set; }

    public int? IdPremio { get; set; }

    public virtual Codigo? IdCodigoNavigation { get; set; }

    public virtual Premio? IdPremioNavigation { get; set; }
}
