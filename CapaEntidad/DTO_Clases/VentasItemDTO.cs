using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO_Clases
{
    public class VentasItemDTO
    {
        public int Id { get; set; }

        public int Idventa { get; set; }

        public int Idproducto { get; set; }

        public double? PrecioUnitario { get; set; }

        public double? Cantidad { get; set; }

        public double? PrecioTotal { get; set; }
    }
}
