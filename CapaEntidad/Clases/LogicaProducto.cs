using Application_Layer.DTO_Clases;
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
            if (productoDTO == null)
                throw new ArgumentNullException(nameof(productoDTO));

            try
            {
                var producto = new Producto
                {
                    Id = productoDTO.Id,
                    Nombre = productoDTO.Nombre,
                    Precio = productoDTO.Precio,
                    Categoria = productoDTO.Categoria,
                };

                db.Productos.Add(producto);
                await db.SaveChangesAsync();
                return producto;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error actualizando la base de datos: {ex.Message}");
                throw new ApplicationException("Error al guardar los datos del producto.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando el producto: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al crear el producto.", ex);
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

        public void Edit(int id, ProductoDTO productoDTO)
        {
            if (productoDTO == null)
                throw new ArgumentNullException(nameof(productoDTO));

            var productoSelec = db.Productos.FirstOrDefault(x => x.Id == id);
            if (productoSelec == null)
                throw new KeyNotFoundException($"No se encontró un producto con el ID {id}.");

            try
            {
                productoSelec.Nombre = productoDTO.Nombre;
                productoSelec.Precio = productoDTO.Precio;
                productoSelec.Categoria = productoDTO.Categoria;
                db.Productos.Update(productoSelec);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al actualizar la base de datos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al actualizar el producto.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar el producto: {ex.Message}");
                throw;
            }
        }

        public List<ProductoDTO> GetEntidad()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener los productos.", ex);
            }
        }

        public async Task<ProductoDTO?> GetById(int id)
        {
            try
            {
                var producto = await db.Productos
                    .Where(x => x.Id == id)
                    .Select(x => new ProductoDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Categoria = x.Categoria
                    })
                    .FirstOrDefaultAsync();

                if (producto != null)
                {
                    return producto;
                }

                Console.WriteLine($"Producto con Id {id} no encontrado.");
                throw new Exception("Producto no encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener el producto.", ex);
            }
        }
    }
}
