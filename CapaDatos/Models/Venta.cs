using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int? Idcliente { get; set; }

    public DateTime? Fecha { get; set; }

    public double? Total { get; set; }

    internal virtual Cliente? IdclienteNavigation { get; set; }

    public required List<Ventasitem> Ventasitems { get; set; }
}
