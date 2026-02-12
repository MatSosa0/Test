using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Data;
using SistemaVentas.DTOs;
using SistemaVentas.Models;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVentas.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVentas()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .Select(v => new VentaResponseDto
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    ClienteId = v.ClienteId,
                    ClienteNombre = v.Cliente.Nombre,
                    Total = v.Total,
                    Detalles = v.Detalles.Select(d => new VentaDetalleResponseDto
                    {
                        ProductoId = d.ProductoId,
                        ProductoNombre = d.Producto.Nombre,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Subtotal
                    }).ToList()
                })
                .ToListAsync();

            return Ok(ventas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .Where(v => v.Id == id)
                .Select(v => new VentaResponseDto
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    ClienteId = v.ClienteId,
                    ClienteNombre = v.Cliente.Nombre,
                    Total = v.Total,
                    Detalles = v.Detalles.Select(d => new VentaDetalleResponseDto
                    {
                        ProductoId = d.ProductoId,
                        ProductoNombre = d.Producto.Nombre,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Subtotal
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (venta == null)
                return NotFound();

            return Ok(venta);
        }


        [HttpPost]
        public async Task<IActionResult> CrearVenta(CrearVentaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
                if (cliente == null)
                    return NotFound("Cliente no existe");

                var venta = new Venta
                {
                    ClienteId = dto.ClienteId,
                    Fecha = DateTime.Now
                };

                decimal total = 0;

                foreach (var item in dto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(item.ProductoId);

                    if (producto == null)
                        return NotFound($"Producto {item.ProductoId} no existe");

                    if (producto.stock < item.Cantidad)
                        return BadRequest($"Stock insuficiente para {producto.Nombre}");

                    var subtotal = producto.Precio * item.Cantidad;

                    var detalle = new VentaDetalle
                    {
                        ProductoId = producto.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.Precio,
                        Subtotal = subtotal
                    };

                    producto.stock -= item.Cantidad;

                    total += subtotal;

                    venta.Detalles.Add(detalle);
                }

                venta.Total = total;

                _context.Ventas.Add(venta);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new VentaResponseDto
                            {
                                Id = venta.Id,
                                Fecha = venta.Fecha,
                                ClienteId = venta.ClienteId,
                                ClienteNombre = cliente.Nombre,
                                Total = venta.Total,
                                Detalles = venta.Detalles.Select(d => new VentaDetalleResponseDto
                                {
                                    ProductoId = d.ProductoId,
                                    ProductoNombre = _context.Productos
                                        .Where(p => p.Id == d.ProductoId)
                                        .Select(p => p.Nombre)
                                        .FirstOrDefault(),
                                    Cantidad = d.Cantidad,
                                    PrecioUnitario = d.PrecioUnitario,
                                    Subtotal = d.Subtotal
                                }).ToList()
                            });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Detalles)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return NotFound();

            _context.VentaDetalles.RemoveRange(venta.Detalles);
            _context.Ventas.Remove(venta);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
