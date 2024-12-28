using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_Clases
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public List<Venta>? ListaVenta { get; set; }
    }
}
