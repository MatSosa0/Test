using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Data;
using SistemaVentas.Models;

namespace SistemaVentas.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            
            if (cliente == null)
            {
                return NotFound();   
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> Put(int id, Cliente cliente)
        {

            if(id!= cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}