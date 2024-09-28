using apiFIDO.Data;
using apiFIDO.Models;
using Microsoft.EntityFrameworkCore;

namespace apiFIDO.DLFido
{
    public class OperacionesBD
    {
        private readonly ApplicationDbContext DBContext;
        respuestaRequest resp;
        public OperacionesBD(ApplicationDbContext dbContext)
        {
            this.DBContext = dbContext;
        }

        #region registroCodigo

        //public async Task<respuestaRequest> autoresActivos(int id = 0, string? nombreAutor = null)
        //{
        //    resp = new respuestaRequest();

        //    try
        //    {
        //    //    var query = from au in DBContext.Autors
        //    //                where au.Estado == 1
        //    //                && au.Id == (id == 0 ? au.Id : id)
        //    //                && au.Nombre.Contains((nombreAutor == null ? au.Nombre : nombreAutor))
        //    //                select au;

        //    //    var List = await query.Select(
        //    //    s => new AutorDTO
        //    //    {
        //    //        Id = s.Id,
        //    //        Nombre = s.Nombre,
        //    //        Estado = s.Estado,
        //    //        FecCreacion = s.FecCreacion
        //    //    }
        //    //).ToListAsync();

        //    //    if (List.Count == 0)
        //    //    {
        //    //        resp.error = 1;
        //    //        resp.mensaje = "No se encontro ningun autor.";
        //    //    }
        //    //    else
        //    //    {
        //    //        resp.error = 0;
        //    //    }

        //    //    resp.data = List;

        //        return resp;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task<respuestaRequest> registrarCliente(Cliente datos)
        //{
        //    resp = new respuestaRequest();



        //    //var nuevoAutor = new Autor()
        //    //{
        //    //    Nombre = datos.Nombre
        //    //};

        //    //try
        //    //{
        //    //    this.DBContext.Autors.Add(nuevoAutor);
        //    //    await DBContext.SaveChangesAsync();

        //    //    resp.error = 0;
        //    //    resp.mensaje = "Autor creado correctamente.";
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    resp.error = 1;
        //    //    resp.mensaje = "Error al crear autor.";
        //    //}

        //    return resp;
        //}

        public async Task<respuestaRequest> validarCodigo(solicitud datosSolicitud)
        {
            resp = new respuestaRequest();

            try
            {
                var query = from vista in DBContext._VW_VALIDA_CODIGO
                            where vista.CODIGO == datosSolicitud.codigoCliente
                            select new
                            {
                                vista.CODIGO,
                                vista.IND_CANJEADO
                            };

                var List = await query.Select(
                s => new solicitud
                {
                    codigoCliente = s.CODIGO,
                    indCanjeado = s.IND_CANJEADO                    
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
        #endregion

    }
}
