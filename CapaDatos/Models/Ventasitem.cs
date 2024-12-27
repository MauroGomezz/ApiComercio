using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class Ventasitem
{
    public int Id { get; set; }

    public int Idventa { get; set; }

    public int Idproducto { get; set; }

    public double? PrecioUnitario { get; set; }

    public double? Cantidad { get; set; }

    public double? PrecioTotal { get; set; }

    internal virtual Producto? IdproductoNavigation { get; set; }

    internal virtual Venta? IdventaNavigation { get; set; }
}
