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
        public int registrarCliente(datosRequest datos)
        {
           int newIdCliente = 0;

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
                DBContext.SaveChanges();


                newIdCliente = nuevoCliente.IdCliente;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return newIdCliente;
        }

        public bool insertarCanje(datosRequest datos)
        {
            bool Inserto = false;
            

            var nuevoCanje = new CanjePremio()
            {
                IdCliente = int.Parse(datos.IdCliente ?? "0"),
                IdCodigoPremio = int.Parse(datos.IdCodigoPremio ?? "0")
            };

            try
            {
                this.DBContext.CanjePremios.Add(nuevoCanje);
                DBContext.SaveChanges();
                Inserto = true;

            }
            catch (Exception)
            {


            }

            return Inserto;
        }

        public async Task<respuestaRequest> registrarClienteCanje (datosRequest datos)
        {
            resp = new respuestaRequest();
            int idCliente;

          
              idCliente = registrarCliente(datos);
              datos.IdCliente = idCliente.ToString();
                if (idCliente == 0)
                {

                    resp.error = 1;
                    resp.mensaje = "Hubo un error al guardar el cliente.";

                } 
                else 
                {


                    if  (insertarCanje(datos))
                    {
                        resp.error = 0;
                        resp.mensaje = "El canje fue realizado correctamente.";
                    }
                    else
                    {

                        resp.error = 1;
                        resp.mensaje = "Hubo un error al canjear el codigo.";


                    }

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
                                p.Src,
                                cp.IdCodigoPremio
                            };

                var List = await query.Select(
                s => new PremioDTO
                {
                    Titulo = s.Titulo,
                    Descripcion = s.Descripcion,
                    Src = s.Src,
                    IdCodigoPremio = s.IdCodigoPremio
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


        //SELECT RAZAS
        public async Task<respuestaRequest> obtenerRazaPerro()
        {
            resp = new respuestaRequest();

            try
            {
                var query = from rp in DBContext.RazaPerros
                            where rp.Estado == 1
                            select new
                            {
                                rp.Nombre,
                                rp.IdRaza
                            };

                var List = await query.Select(
                s => new RazaPerroDTO
                {
                    IdRaza = s.IdRaza,
                    Nombre = s.Nombre
                    
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
