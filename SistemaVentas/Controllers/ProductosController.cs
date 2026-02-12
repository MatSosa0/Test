using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Data;
using SistemaVentas.Models;

namespace SistemaVentas.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            
            if (productos == null)
            {
                return NotFound();   
            }

            return Ok(productos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Producto producto)
        {

            if(id!= producto.Id)
            {
                return BadRequest("El ID no coincide");
            }

            var productoDB = await _context.Productos.FindAsync(id);
            if (productoDB == null)
            {
                return NotFound();
            }

            productoDB.Nombre = producto.Nombre;
            productoDB.Precio = producto.Precio;
            productoDB.stock = producto.stock;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}