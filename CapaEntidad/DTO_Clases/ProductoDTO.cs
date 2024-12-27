using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO_Clases
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public double? Precio { get; set; }

        public string? Categoria { get; set; }
    }
}
