using bibliotecaAPI.Entities;
using bibliotecaAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using bibliotecaAPI.DLBiblioteca;

namespace bibliotecaAPI.Controllers
{
    [Route("api/biblioteca")]
    [ApiController]
    [EnableCors("MYCORS")]
    public class BibliotecaController : ControllerBase
    {
        private readonly BibliotecaContext DBContext;
        RespuestaDTO resp;
        OperacionesBD ops;
        public BibliotecaController(BibliotecaContext DBContext)
        {
            this.DBContext = DBContext;
        }

        #region Autores
        [HttpGet]
        [Route("Autores")]
        public async Task<ActionResult<RespuestaDTO>> Autores()
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.autoresActivos();

            return Ok(resp);
                
        }

        [HttpGet]
        [Route("AutorPorId")]
        public async Task<ActionResult<RespuestaDTO>> AutorPorId(int id)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.autoresActivos(id:id);

            return Ok(resp);
        }

        [HttpGet]
        [Route("AutorPorNombre")]
        public async Task<ActionResult<RespuestaDTO>> AutorPorNombre(string nombre)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.autoresActivos(nombreAutor:nombre);

            return Ok(resp);
        }

        [HttpGet]
        [Route("buscarAutores")]
        public async Task<ActionResult<RespuestaDTO>> buscarAutores(int id, string? nombre)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.autoresActivos(id: id, nombreAutor: nombre);

            return Ok(resp);
        }

        [HttpPost]
        [Route("agregarAutor")]
        public async Task<ActionResult<RespuestaDTO>> agregarAutor(AutorDTO nuevoAutor)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.agregarAutor(nuevoAutor);

            return Ok(resp);
        }

        [HttpPost]
        [Route("modificarAutor")]
        public async Task<ActionResult<RespuestaDTO>> modificarAutor(AutorDTO datosAutor)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.modificarAutor(datosAutor);

            return Ok(resp);
        }

        #endregion

        #region Libros
        [HttpGet]
        [Route("Libros")]
        public async Task<ActionResult<RespuestaDTO>> Libros()
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.librosActivos();

            return Ok(resp);
        }

        [HttpGet]
        [Route("LibroPorId")]
        public async Task<ActionResult<RespuestaDTO>> LibroPorId(int id)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.librosActivos(id:id);

            return Ok(resp);
        }

        [HttpGet]
        [Route("LibroPorNombre")]
        public async Task<ActionResult<RespuestaDTO>> LibroPorNombre(string titulo)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.librosActivos(tituloLibro : titulo);

            return Ok(resp);
        }

        [HttpPost]
        [Route("agregarLibro")]
        public async Task<ActionResult<RespuestaDTO>> agregarLibro(LibroDTO nuevoLibro)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.agregarLibro(nuevoLibro);

            return Ok(resp);
        }

        [HttpPost]
        [Route("modificarLibro")]
        public async Task<ActionResult<RespuestaDTO>> modificarLibro(LibroDTO datosLibro)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.modificarLibro(datosLibro);

            return Ok(resp);
        }
        
        [HttpGet]
        [Route("buscarLibros")]
        public async Task<ActionResult<RespuestaDTO>> buscarLibros(int id, string? titulo, int idAutor)
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.librosActivos(id: id, tituloLibro: titulo, idAutor=idAutor);

            return Ok(resp);
        }
        #endregion

        #region Editoriales
        [HttpGet]
        [Route("Editoriales")]
        public async Task<ActionResult<RespuestaDTO>> Editoriales()
        {
            resp = new RespuestaDTO();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.editorialesActivas();

            return Ok(resp);
        }
        #endregion
    }
}
