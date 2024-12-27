using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public double? Precio { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<Ventasitem> Ventasitems { get; } = new List<Ventasitem>();
}
