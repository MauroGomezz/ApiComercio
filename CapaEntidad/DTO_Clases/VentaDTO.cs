using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO_Clases
{
    public class VentaDTO
    {
        public int Id { get; set; }

        public int? Idcliente { get; set; }

        public DateTime? Fecha { get; set; }

        public double? Total { get; set; }

        public required List<VentasItemDTO> Ventasitems { get; set; }
    }
}
