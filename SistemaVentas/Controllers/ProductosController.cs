using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Models;

namespace SistemaVentas.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private static readonly List<Producto> _productos = new();
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productos);
        }

        [HttpPost]
        public IActionResult Post(Producto producto)
        {
            producto.Id = _productos.Count + 1;
            _productos.Add(producto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id}, producto);
        }

    }
}