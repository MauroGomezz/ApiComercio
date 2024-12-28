using ApplicationLayer.Clases;
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
    public class LogicaVenta : LogicaVentaItem, IGeneric<VentaDTO, Venta>
    {
        public LogicaVenta(PruebademoContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<Venta> Create(VentaDTO ventaDTO)
        {
            if (ventaDTO == null)
                throw new ArgumentNullException(nameof(ventaDTO));

            var ventaItem = CreateItem(ventaDTO.Ventasitems) ?? new List<Ventasitem>();

            var total = ventaItem.Sum(item => item.PrecioTotal);
            try
            {
                var venta = new Venta
                {
                    Id = ventaDTO.Id,
                    Fecha = ventaDTO.Fecha,
                    Total = total,
                    Idcliente = ventaDTO.Idcliente,
                    Ventasitems = ventaItem,
                };

                db.Ventas.Add(venta);
                await db.SaveChangesAsync();
                return venta;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error actualizando la base de datos: {ex.Message}");
                throw new ApplicationException("Error al guardar los datos de la venta.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando la venta: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al crear la venta.", ex);
            }
        }

        public void Delete(int id)
        {
            var ventaSelec = db.Ventas.Where(v => v.Id == id).FirstOrDefault();
            if (ventaSelec != null)
            {
                db.Ventas.Remove(ventaSelec);
                db.SaveChanges();
            }
        }

        public void Edit(int id, VentaDTO ventaDTO)
        {
            if (ventaDTO == null)
                throw new ArgumentNullException(nameof(ventaDTO));

            var ventaSelec = db.Ventas.FirstOrDefault(x => x.Id == id);
            if (ventaSelec == null)
                throw new KeyNotFoundException($"No se encontró una venta con el ID {id}.");

            try
            {
                var ventaItem = EditItem(ventaDTO.Ventasitems) ?? new List<Ventasitem>();
                ventaSelec.Fecha = ventaDTO.Fecha ?? throw new ArgumentException("La fecha no puede ser nula.", nameof(ventaDTO.Fecha));
                ventaSelec.Ventasitems = ventaItem;
                ventaSelec.Total = ventaSelec.Ventasitems.Sum(item => item.PrecioTotal);
                db.Ventas.Update(ventaSelec);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al actualizar la base de datos: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al actualizar la venta.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar la venta: {ex.Message}");
                throw;
            }
        }

        public List<VentaDTO> GetEntidad()
        {
            try
            {
                var ventas = db.Ventas
                    .Include(v => v.Ventasitems)
                    .Select(x => new VentaDTO
                    {
                        Id = x.Id,
                        Fecha = x.Fecha,
                        Idcliente = x.Idcliente,
                        Total = x.Total,
                        Ventasitems = x.Ventasitems.Select(item => new Ventasitem
                        {
                            Id = item.Id,
                            Idventa = item.Idventa,
                            Idproducto = item.Idproducto,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario,
                            PrecioTotal = item.PrecioTotal
                        }).ToList()
                    })
                    .ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las ventas: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener las ventas.", ex);
            }
        }
        public async Task<VentaDTO?> GetById(int id)
        {
            try
            {
                var ventas = await db.Ventas
                    .Where(x => id == x.Id)
                    .Include(v => v.Ventasitems)
                    .Select(x => new VentaDTO
                    {
                        Id = x.Id,
                        Fecha = x.Fecha,
                        Idcliente = x.Idcliente,
                        Total = x.Total,
                        Ventasitems = x.Ventasitems.Select(item => new Ventasitem
                        {
                            Id = item.Id,
                            Idventa = item.Idventa,
                            Idproducto = item.Idproducto,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario,
                            PrecioTotal = item.PrecioTotal
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (ventas != null)
                {
                    return ventas;
                }

                Console.WriteLine($"Venta con Id {id} no encontrado.");
                throw new Exception("Venta no encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la venta: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al obtener la venta.", ex);
            }
        }
    }
}
