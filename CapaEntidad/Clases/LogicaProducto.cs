using ApplicationLayer.DTO_Clases;
using CapaDatos.Models;
using CapaEntidad.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Clases
{
    public class LogicaProducto : IGeneric<ProductoDTO, Producto>
    {
        public PruebademoContext db;
        public LogicaProducto(PruebademoContext db) { this.db = db; }

        public async Task<Producto> Create(ProductoDTO productoDTO)
        {
            try
            {
                if (productoDTO != null)
                {
                    var producto = new Producto()
                    {
                        Id = productoDTO.Id,
                        Categoria = productoDTO.Categoria,
                        Nombre = productoDTO.Nombre,
                        Precio = productoDTO.Precio,
                    };
                    var obj = db.Productos.Add(producto);
                    await db.SaveChangesAsync();
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            var productoSelec = db.Productos.Where(x => x.Id == id).FirstOrDefault();
            if (productoSelec != null)
            {
                db.Productos.Remove(productoSelec);
                db.SaveChanges();
            };
        }
        public void Edit(int id, ProductoDTO producto)
        {
            var productoSelec = db.Productos.Where(x => x.Id == id).FirstOrDefault();
            if (productoSelec != null)
            {
                productoSelec.Nombre = producto.Nombre;
                productoSelec.Precio = producto.Precio;
                productoSelec.Categoria = producto.Categoria;
                db.SaveChanges();
                db.Productos.Update(productoSelec);
            };
        }


        public List<ProductoDTO> GetEntidad()
        {
            var producto = db.Productos.Select(x => new ProductoDTO()
            {
                Id = x.Id,
                Categoria = x.Categoria,
                Nombre = x.Nombre,
                Precio = x.Precio
            });
            return producto.ToList();
        }
        public async Task<ProductoDTO?> GetById(int id)
        {
            var producto = await db.Productos.Where(x => id == x.Id).FirstOrDefaultAsync();
            if (producto == null)
                return null;
            var productoDTO = new ProductoDTO
            {
                Id = id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Categoria = producto.Categoria
            };
            return productoDTO;
        }
    }
}
