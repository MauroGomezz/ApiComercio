using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Interfaces
{
    public interface IGeneric<T,V>
    {
        List<T> GetEntidad();
        public void Edit(int id, T entity);
        public Task<V> Create(T entity);
        public Task<T?> GetById(int id);
        public void Delete(int id);
    }
}
