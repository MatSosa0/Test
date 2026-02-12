namespace SistemaVentas.DTOs
{
    public class VentaResponseDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<VentaDetalleResponseDto> Detalles { get; set; } = new();
    }
}
