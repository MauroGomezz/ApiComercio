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
    public class LogicaCliente : IGeneric<ClienteDTO, Cliente>
    {
        private PruebademoContext db;
        public LogicaCliente(PruebademoContext db) { this.db = db; }

        public async Task<Cliente> Create(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null || string.IsNullOrWhiteSpace(clienteDTO.Name) || string.IsNullOrWhiteSpace(clienteDTO.Correo))
            {
                throw new ArgumentException("El clienteDTO es inválido o tiene campos requeridos vacíos.");
            }

            try
            {
                var cliente = new Cliente
                {
                    Id = clienteDTO.Id,
                    Name = clienteDTO.Name,
                    Correo = clienteDTO.Correo,
                    Telefono = clienteDTO.Telefono,
                };

                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                return cliente;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error actualizando la base de datos: {ex.Message}");
                throw new ApplicationException("Error al guardar los datos del cliente.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando el cliente: {ex.Message}");
                throw new ApplicationException("Ocurrió un error al crear el cliente.", ex);
            }
        }

        public void Delete(int id)
        {
            var clienteSelec = db.Clientes.Where(x => x.Id == id).FirstOrDefault();
            if (clienteSelec != null)
            {
                db.Clientes.Remove(clienteSelec);
                db.SaveChanges();
            };
        }

        public void Edit(int id, ClienteDTO cliente)
        {
            var clienteSelec = db.Clientes.Where(x => x.Id == id).FirstOrDefault();
            if (clienteSelec != null)
            {
                clienteSelec.Name = cliente.Name;
                clienteSelec.Telefono = cliente.Telefono;
                clienteSelec.Correo = cliente.Correo;
                db.SaveChanges();
                db.Clientes.Update(clienteSelec);
            };
        }

        public List<ClienteDTO> GetEntidad()
        {
            var clientes = db.Clientes.Select(x => new ClienteDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Telefono = x.Telefono,
                Correo = x.Correo,
            });
            return clientes.ToList();
        }

        public async Task<ClienteDTO?> GetById(int id)
        {
            try
            {
                var cliente = await db.Clientes
                    .Where(x => x.Id == id)
                    .Select(x => new ClienteDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Correo = x.Correo,
                        Telefono = x.Telefono,
                        ListaVenta = x.Venta
                    })
                    .FirstOrDefaultAsync();

                if (cliente != null)
                {
                    return cliente;
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
