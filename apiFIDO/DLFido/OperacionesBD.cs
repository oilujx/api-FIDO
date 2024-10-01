using apiFIDO.Data;
using apiFIDO.Models;
using Microsoft.EntityFrameworkCore;

namespace apiFIDO.DLFido
{
    public class OperacionesBD
    {
        private readonly ApplicationDbContext DBContext;
        respuestaRequest? resp;
        public OperacionesBD(ApplicationDbContext dbContext)
        {
            this.DBContext = dbContext;
        }
        #region registroCodigo        
        public async Task<respuestaRequest> registrarCliente(datosRequest datos)
        {
            resp = new respuestaRequest();

            var nuevoCliente = new Cliente()
            {
                Nombre = datos.Nombre,
                Telefono = datos.Telefono,
                Direccion = datos.Direccion,
                Correo = datos.Correo,
                IdRaza = int.Parse(datos.Id_Raza ?? "0"),
                PesoPerro = decimal.Parse(datos.Peso_Perro ?? "0")
            };

            try
            {
                this.DBContext.Clientes.Add(nuevoCliente);
                await DBContext.SaveChangesAsync();

                resp.error = 0;
                resp.mensaje = "Cliente creado correctamente.";
            }
            catch (Exception)
            {

                resp.error = 1;
                resp.mensaje = "Error al crear cliente.";
            }

            return resp;
        }
        public async Task<respuestaRequest> validarCodigo(datosRequest datosSolicitud)
        {
            resp = new respuestaRequest();

            try
            {
                var query = from vista in DBContext.VwValidaCodigos
                            where vista.Codigo == datosSolicitud.codigoCliente
                            select new
                            {
                                vista.Codigo,
                                vista.IndCanjeado
                            };

                var List = await query.Select(
                s => new datosRequest
                {
                    codigoCliente = s.Codigo,
                    indCanjeado = s.IndCanjeado                    
                }
                    ).ToListAsync();

                if (List.Count > 0)
                {
                    if (List[0].indCanjeado == "S")
                    {
                        resp.error = 1;
                        resp.mensaje = "El código ingresado ya fue canjeado.";
                    }
                    else
                    {
                        resp.error = 0;
                        resp.mensaje = "Código valido.";
                    }    
                }
                else
                {
                    resp.mensaje = "El código ingresado no es existe.";
                    resp.error = 1;                   
                }
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message;
                resp.error = 1;
            }

            return resp;
        }
        public async Task<respuestaRequest> obtenerPremiosXCodigo(datosRequest datosSolicitud)
        {
            resp = new respuestaRequest();

            try
            {
                var query = from p in DBContext.Premios
                            join cp in DBContext.CodigoPremios on p.IdPremio equals cp.IdPremio
                            join c in DBContext.Codigos on cp.IdCodigo equals c.IdCodigo
                            where c.Codigo1 == datosSolicitud.codigoCliente                            
                            select new
                            {
                                p.Titulo,
                                p.Descripcion,
                                p.Src
                            };

                var List = await query.Select(
                s => new datosRequest
                {
                    Titulo = s.Titulo,
                    Descripcion = s.Descripcion,
                    Src = s.Src
                }
                    ).ToListAsync();

                resp.error = 0;
                resp.mensaje = "";
                resp.data = List;
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message;
                resp.error = 1;
            }

            return resp;
        }

        #endregion

    }
}
