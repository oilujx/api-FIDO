using System;
using System.Collections.Generic;

namespace apiFIDO.Models;

public partial class CanjePremio
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdCodigoPremio { get; set; }

    public DateTime? FecCanje { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual CodigoPremio? IdCodigoPremioNavigation { get; set; }
}
