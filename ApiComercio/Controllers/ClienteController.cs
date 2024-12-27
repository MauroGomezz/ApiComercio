using Application_Layer.DTO_Clases;
using CapaDatos.Models;
using CapaEntidad.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiComercio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IGeneric<ClienteDTO, Cliente> _cliente;

        public ClienteController(IGeneric<ClienteDTO, Cliente> cliente)
        {
            _cliente = cliente;
        }

        [HttpGet]
        public IEnumerable<ClienteDTO> Get()
        {
            return _cliente.GetEntidad();
        }

        [HttpGet("{id:int}")]
        public async Task<ClienteDTO?> GetById(int id)
        {
            var result = await _cliente.GetById(id);
            return result;
        }

        [HttpPost]
        public ActionResult Create(ClienteDTO cliente)
        {
            _cliente.Create(cliente);
            return CreatedAtAction("Get", new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id:int}")]
        public ActionResult Edit(int id, ClienteDTO clienteupdate)
        {
            _cliente.Edit(id, clienteupdate);
            return CreatedAtAction("Get", new { id = clienteupdate.Id }, clienteupdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _cliente.Delete(id);
            return Ok("Eliminado Correctamente");
        }

    }
}
