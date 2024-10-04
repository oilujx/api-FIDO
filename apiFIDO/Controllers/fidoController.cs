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

        [HttpPost]
        [Route("ConsultarPremio")]
        public async Task<ActionResult<respuestaRequest>> ConsultarPremio(datosRequest datos)
        {
            resp = new respuestaRequest();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.obtenerPremiosXCodigo(datos);

            return Ok(resp);
        }

        [HttpGet]
        [Route("obtenerRazas")]
        public async Task<ActionResult<respuestaRequest>> obtenerRazas()
        {
            resp = new respuestaRequest();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.obtenerRazaPerro();

            return Ok(resp);
        }

        [HttpPost]
        [Route("CanjearPremio")]
        public async Task<ActionResult<respuestaRequest>> CanjearPremio(datosRequest datos)
        {
            resp = new respuestaRequest();
            ops = new OperacionesBD(this.DBContext);

            resp = await ops.registrarClienteCanje(datos);

            return Ok(resp);
        }
    }
}
