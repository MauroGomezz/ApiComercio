using ApplicationLayer.DTO_Clases;
using CapaDatos.Models;
using CapaEntidad.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiComercio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGeneric<ProductoDTO, Producto> _Producto;

        public ProductoController(IGeneric<ProductoDTO, Producto> producto)
        {
            _Producto = producto;
        }

        [HttpGet]
        public IEnumerable<ProductoDTO> Get()
        {
            return _Producto.GetEntidad();
        }

        [HttpGet("{id:int}")]
        public async Task<ProductoDTO?> GetById(int id)
        {
            var result = await _Producto.GetById(id);
            return result;
        }

        [HttpPost]
        public ActionResult Create(ProductoDTO producto)
        {
            _Producto.Create(producto);
            return CreatedAtAction("Get", new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Edit(int id, ProductoDTO productoUpdate)
        {
            _Producto.Edit(id, productoUpdate);
            return CreatedAtAction("Get", new { id = productoUpdate.Id }, productoUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _Producto.Delete(id);
            return Ok("Eliminado Correctamente");
        }
    }
}
