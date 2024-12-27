using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IVentaItem<T>
    {
        public List<Ventasitem> EditItem(List<T> entity);
        public List<Ventasitem> CreateItem(List<T> entity);
        public void DeleteItem(int id, int id2);
    }
}
