namespace apiFIDO.Models
{
    public class respuestaRequest
    {
        public int error { get; set; }
        public string mensaje { get; set; } = null!;
        public object data { get; set; } = null!;
    }
}
