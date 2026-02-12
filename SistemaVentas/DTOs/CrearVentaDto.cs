namespace SistemaVentas.DTOs
{
    public class CrearVentaDto
    {
        public int ClienteId { get; set; }
        public List<CrearVentaDetalleDto> Detalles { get; set; } = new();
    }
}