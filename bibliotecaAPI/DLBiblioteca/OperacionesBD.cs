using bibliotecaAPI.Entities;
using bibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace bibliotecaAPI.DLBiblioteca
{
    public class OperacionesBD
    {
        private readonly BibliotecaContext DBContext;
        RespuestaDTO resp;
        public OperacionesBD(BibliotecaContext DBContext)
        {
            this.DBContext = DBContext;
        }

        #region Autores
        public async Task<RespuestaDTO> autoresActivos(int id = 0, string? nombreAutor = null)
        {
            resp = new RespuestaDTO();

            try
            {
                var query = from au in DBContext.Autors
                            where au.Estado == 1
                            && au.Id == (id == 0 ? au.Id :id)
                            && au.Nombre.Contains((nombreAutor == null ? au.Nombre : nombreAutor))
                            select au;

                var List = await query.Select(
                s => new AutorDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Estado = s.Estado,
                    FecCreacion = s.FecCreacion
                }
            ).ToListAsync();

                if (List.Count == 0)
                {
                    resp.error = 1;
                    resp.mensaje = "No se encontro ningun autor.";
                }
                else
                {
                    resp.error = 0;
                }

                resp.data = List;

                return resp;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<RespuestaDTO> agregarAutor(AutorDTO datos)
        {
            resp = new RespuestaDTO();

            var nuevoAutor = new Autor()
            {
                Nombre = datos.Nombre                
            };

            try
            {
                this.DBContext.Autors.Add(nuevoAutor);
                await DBContext.SaveChangesAsync();

                resp.error = 0;
                resp.mensaje = "Autor creado correctamente.";
            }
            catch (Exception)
            {

                resp.error = 1;
                resp.mensaje = "Error al crear autor.";
            }

            return resp;
        }
        public async Task<RespuestaDTO> modificarAutor(AutorDTO datos)
        {
            resp = new RespuestaDTO();

            var autorExistente = await DBContext.Autors.FirstOrDefaultAsync(aut => aut.Id == datos.Id);

            if (datos.Estado == 1)
            {
                autorExistente.Nombre = datos.Nombre;
                autorExistente.Estado = datos.Estado;
                resp.mensaje = "Autor modificado correctamente.";
            }
            else
            {
                //validar que el autor exista en un libro activo
                var autorEnLibroActivo = await DBContext.Libros.FirstOrDefaultAsync(lib => lib.Autor == datos.Id && lib.Estado == 1);

                if (autorEnLibroActivo != null)
                {
                    resp.error = 1;
                    resp.mensaje = "El autor existe en un libro activo.";
                    return resp;
                }

                autorExistente.Estado = datos.Estado;
                resp.mensaje = "Autor eliminado correctamente.";
            }

            try
            {
                await DBContext.SaveChangesAsync();

                resp.error = 0;
                
            }
            catch (Exception)
            {

                resp.error = 1;
                resp.mensaje = "Error al modificar autor.";
            }

            return resp;
        }
        #endregion

        #region Libros
        public async Task<RespuestaDTO> librosActivos(int id = 0, string tituloLibro = "", int idAutor = 0)
        {
            resp = new RespuestaDTO();

            try
            {
                var query = from lb in DBContext.Libros
                            join edi in DBContext.Editorials on lb.Editorial equals edi.Id
                            join aut in DBContext.Autors on lb.Autor equals aut.Id
                            where lb.Estado == 1
                            && lb.Isbn == (id == 0 ? lb.Isbn : id)                           
                            && lb.Titulo.Contains((tituloLibro == null ? lb.Titulo : tituloLibro))
                            && lb.Autor == (idAutor == 0 ? lb.Autor : idAutor)
                            select new
                            {
                                lb.Isbn, lb.Titulo, idEditorial = edi.Id,nombreEditorial = edi.Nombre, lb.AnioEdicion, 
                                lb.PrecioPrestamo, lb.FecCreacion, lb.Estado, idAutor = aut.Id,
                                nombreAutor = aut.Nombre
                            };

                var List = await query.Select(
                s => new LibroDTO
                {
                    isbn = s.Isbn,
                    titulo = s.Titulo,
                    editorial = s.nombreEditorial,
                    anioEdicion = s.AnioEdicion,
                    precioPrestamo = s.PrecioPrestamo,
                    fecCreacion = s.FecCreacion,
                    estado = s.Estado,
                    autor = s.nombreAutor,
                    idEditorial = s.idEditorial,
                    idAutor = s.idAutor
                }
            ).ToListAsync();

                if (List.Count == 0)
                {
                    resp.error = 1;
                    resp.mensaje = "No se encontro ningun libro.";
                }
                else
                {
                    resp.error = 0;
                }

                resp.data = List;

                return resp;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<RespuestaDTO> agregarLibro(LibroDTO datos)
        {
            resp = new RespuestaDTO();

            var nuevoLibro = new Libro()
            {
                Titulo = datos.titulo,
                Editorial = datos.idEditorial,
                AnioEdicion = datos.anioEdicion,
                Autor = datos.idAutor,
                PrecioPrestamo = datos.precioPrestamo
            };

            try
            {
                this.DBContext.Libros.Add(nuevoLibro);
                await DBContext.SaveChangesAsync();

                resp.error = 0;
                resp.mensaje = "Libro creado correctamente.";
            }
            catch (Exception)
            {

                resp.error = 1;
                resp.mensaje = "Error al crear libro.";
            }

            return resp;
        }
        public async Task<RespuestaDTO> modificarLibro(LibroDTO datos)
        {
            resp = new RespuestaDTO();

            var libroExistente = await DBContext.Libros.FirstOrDefaultAsync(lib => lib.Isbn == datos.isbn);

            libroExistente.Titulo  = datos.titulo;
            libroExistente.Editorial = datos.idEditorial;
            libroExistente.AnioEdicion = datos.anioEdicion;
            libroExistente.Autor = datos.idAutor;
            libroExistente.PrecioPrestamo = datos.precioPrestamo;
            libroExistente.Estado = datos.estado;

            try
            {
                await DBContext.SaveChangesAsync();

                resp.error = 0;

                if (libroExistente.Estado == 1)
                {
                    resp.mensaje = "Libro modificado correctamente.";
                }
                else
                {
                    resp.mensaje = "Libro eliminado correctamente.";
                }
                
            }
            catch (Exception)
            {

                resp.error = 1;
                resp.mensaje = "Error al modificar libro.";
            }

            return resp;
        }
        #endregion

        #region Editoriales
        public async Task<RespuestaDTO> editorialesActivas()
        {
            resp = new RespuestaDTO();

            try
            {
                var query = from au in DBContext.Editorials
                            where au.Estado == 1                            
                            select au;

                var List = await query.Select(
                s => new EditorialDTO
                {
                    id = s.Id,
                    nombre = s.Nombre                    
                }
            ).ToListAsync();

                if (List.Count == 0)
                {
                    resp.error = 1;
                    resp.mensaje = "No se encontro ninguna editorial.";
                }
                else
                {
                    resp.error = 0;
                }

                resp.data = List;

                return resp;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
