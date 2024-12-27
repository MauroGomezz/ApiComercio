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

            var venta = new Venta
            {
                Id = ventaDTO.Id,
                Fecha = ventaDTO.Fecha,
                Total = total,
                Idcliente = ventaDTO.Idcliente,
                Ventasitems = ventaItem,
            };

            var obj = db.Ventas.Add(venta);
            await db.SaveChangesAsync();
            return obj.Entity;
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
            var ventaSelec = db.Ventas.Where(x => x.Id == id).FirstOrDefault();
            var ventaItem = EditItem(ventaDTO.Ventasitems) ?? new List<Ventasitem>();
            if (ventaSelec != null)
            {
                var total = ventaSelec.Ventasitems.Sum(item => item.PrecioTotal);
                ventaSelec.Fecha = ventaDTO.Fecha;
                ventaSelec.Total = total;
                ventaSelec.Ventasitems = ventaItem;
                db.SaveChanges();
                db.Ventas.Update(ventaSelec);
            }
        }

        public List<VentaDTO> GetEntidad()
        {
            try
            {
                var ventas = db.Ventas.Select(x => new VentaDTO()
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    Idcliente = x.Idcliente,
                    Total = x.Total,
                    Ventasitems = x.Ventasitems
                });
                return ventas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<VentaDTO?> GetById(int id)
        {
            var ventas = await db.Ventas.Select(x => new VentaDTO()
            {
                Id = x.Id,
                Fecha = x.Fecha,
                Idcliente = x.Idcliente,
                Total = x.Total,
                Ventasitems = x.Ventasitems
            }).Where(x => id == x.Id).FirstOrDefaultAsync();
            return ventas;
        }
    }
}
