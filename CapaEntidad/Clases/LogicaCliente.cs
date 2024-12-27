using Application_Layer.DTO_Clases;
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
            try
            {
                if (clienteDTO != null)
                {
                    var cliente = new Cliente()
                    {
                        Id = clienteDTO.Id,
                        Name = clienteDTO.Name,
                        Correo = clienteDTO.Correo,
                        Telefono = clienteDTO.Telefono,
                    };
                    var obj = db.Clientes.Add(cliente);
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

        public async Task<ClienteDTO?> GetById(int id)
        {
            var cliente = await db.Clientes.Where(x =>  id == x.Id).FirstOrDefaultAsync();
            if (cliente == null)
                return null;
            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Name = cliente.Name,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono,
            };
            return clienteDTO;
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
    }
}
