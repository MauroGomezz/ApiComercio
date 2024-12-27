using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Venta> Venta { get; } = new List<Venta>();
}
