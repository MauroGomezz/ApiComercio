using ApplicationLayer.DTO_Clases;
using ApplicationLayer.Interfaces;
using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Clases
{
    public class LogicaVentaItem : IVentaItem<Ventasitem>
    {
        public PruebademoContext db;
        public LogicaVentaItem(PruebademoContext db) { this.db = db; }

        public List<Ventasitem> CreateItem(List<Ventasitem>? ventaiItemDTO)
        {
            if (ventaiItemDTO == null || !ventaiItemDTO.Any())
                return new List<Ventasitem>();

            var itemsVenta = new List<Ventasitem>();

            foreach (var item in ventaiItemDTO)
            {
                var producto = db.Productos
                    .Where(x => item.Idproducto == x.Id)
                    .Select(x => x.Precio)
                    .FirstOrDefault();

                if (producto <= 0)
                {
                    throw new Exception($"El producto con ID {item.Idproducto} no existe.");
                }

                var newItem = new Ventasitem
                {
                    Id = item.Id,
                    Cantidad = item.Cantidad,
                    Idproducto = item.Idproducto,
                    Idventa = item.Idventa,
                    PrecioUnitario = producto,
                    PrecioTotal = producto * item.Cantidad
                };
                itemsVenta.Add(newItem);
            }
            return itemsVenta;
        }

        public void DeleteItem(int id, int id2)
        {
            var itemSelec = db.Ventasitems.Where(i => i.Id == id).FirstOrDefault();
            if (itemSelec != null)
            {
                db.Ventasitems.Remove(itemSelec);
                db.SaveChanges();
            }
        }

        public List<Ventasitem> EditItem(List<Ventasitem>? ventaiItemDTO)
        {
            if (ventaiItemDTO == null || !ventaiItemDTO.Any())
                return new List<Ventasitem>();

            var itemsVenta = new List<Ventasitem>();

            using var transaction = db.Database.BeginTransaction();
            try
            {
                foreach (var item in ventaiItemDTO)
                {
                    var ventaItem = db.Ventasitems.FirstOrDefault(x => x.Id == item.Id);
                    var producto = db.Productos
                        .Where(x => item.Idproducto == x.Id)
                        .Select(x => x.Precio)
                        .FirstOrDefault();

                    if (producto <= 0)
                    {
                        throw new Exception($"El producto con ID {item.Idproducto} no existe.");
                    }

                    if (ventaItem != null)
                    {
                        ventaItem.Idproducto = item.Idproducto;
                        ventaItem.Cantidad = item.Cantidad;
                        ventaItem.PrecioUnitario = producto;
                        ventaItem.PrecioTotal = producto * item.Cantidad;

                        itemsVenta.Add(ventaItem);
                    }
                    else
                    {
                        var newItem = new Ventasitem
                        {
                            Idproducto = item.Idproducto,
                            Cantidad = item.Cantidad,
                            Idventa = item.Idventa,
                            PrecioUnitario = producto,
                            PrecioTotal = producto * item.Cantidad
                        };
                        itemsVenta.Add(newItem);
                    }
                }

                db.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            return itemsVenta;
        }
    }
}
