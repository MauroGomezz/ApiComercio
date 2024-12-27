using CapaDatos.Models;
using ApplicationLayer.DTO_Clases;
using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.Interfaces;
using CapaEntidad.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiComercio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IGeneric<VentaDTO, Venta> _venta;

        public VentasController(IGeneric<VentaDTO, Venta> venta)
        {
            _venta = venta;
        }

        [HttpGet]
        public IEnumerable<VentaDTO> Get()
        {
            return _venta.GetEntidad();
        }

        [HttpGet("{id:int}")]
        public async Task<VentaDTO?> GetById(int id)
        {
            var reult = await _venta.GetById(id);
            return reult;
        }

        [HttpPost]
        public ActionResult Create(VentaDTO ventaDTO)
        {
            _venta.Create(ventaDTO);
            return CreatedAtAction("Get", new { id = ventaDTO.Id }, ventaDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, VentaDTO ventaUpdate)
        {
            _venta.Edit(id, ventaUpdate);
            return CreatedAtAction("Get", new { id = ventaUpdate.Id }, ventaUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _venta.Delete(id);
            return Ok("Eliminado Correctamente");
        }
    }
}
