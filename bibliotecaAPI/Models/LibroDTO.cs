namespace bibliotecaAPI.Models
{
    public class LibroDTO
    {
        public int isbn { get; set; }
        public string? titulo { get; set; }
        public string? editorial { get; set; }
        public int idEditorial { get; set; }
        public int? anioEdicion { get; set; }
        public string? autor { get; set; }
        public int idAutor { get; set; }
        public decimal? precioPrestamo { get; set; }
        public DateTime? fecCreacion { get; set; }
        public ulong? estado { get; set; }
    }
}
