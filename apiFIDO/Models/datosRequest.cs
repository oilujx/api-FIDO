namespace apiFIDO.Models
{
    public class datosRequest
    {
        #region datosCanje
        public string? codigoCliente { get; set; }
        public string? indCanjeado { get; set; }
        public string? IdCodigoPremio { get; set; }
        public string? IdCliente { get; set; }


        #endregion

        #region datosRegistro
        public string? Nombre {  get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? Id_Raza { get; set; }
        public string? Peso_Perro { get; set; }

        #endregion

       
    }
}
