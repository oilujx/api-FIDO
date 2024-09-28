using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class CanjePremio
{
    public int? IdCliente { get; set; }

    public int? IdCodigoPremio { get; set; }

    public DateOnly? FecCanje { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual CodigoPremio? IdCodigoPremioNavigation { get; set; }
}
