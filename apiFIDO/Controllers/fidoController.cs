using apiFIDO.Data;
using apiFIDO.DLFido;
using apiFIDO.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiFIDO.Controllers
{
    [Route("api/fido")]
    [ApiController]
    [EnableCors("MYCORS")]
    public class fidoController : Controller
    {
        private readonly ApplicationDbContext DBContext;
        respuestaRequest resp;
        OperacionesBD ops;
        public fidoController(ApplicationDbContext dbContext) 
        { 
            DBContext = dbContext; 
        }

        [HttpPost]
        [Route("validarCodigo")]
        public async Task<ActionResult<respuestaRequest>> validarCodigo(datosRequest datos)
        {
            resp = new respuestaRequest();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.validarCodigo(datos);

            return Ok(resp);
        }
    }
}
