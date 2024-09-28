namespace bibliotecaAPI.Models
{
    public class RespuestaDTO
    {
        public int error { get; set; }
        public string mensaje { get; set; } = null!;
        public object data { get; set; } = null!;
    }
}
