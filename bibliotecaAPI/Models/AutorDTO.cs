namespace bibliotecaAPI.Models
{
    public class AutorDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }       
        public ulong? Estado { get; set; }
        public DateTime? FecCreacion { get; set; }
    }
}
