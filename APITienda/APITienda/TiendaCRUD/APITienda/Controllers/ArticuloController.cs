using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Entitys;

namespace APITienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloServices _articuloServices;

        public ArticuloController(IArticuloServices articuloServices)
        {
            _articuloServices = articuloServices;
        }
        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<IActionResult> ObternerTodo()
        {
            try
            {
                var rsp = await _articuloServices.ObtenerTodo();
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            try
            {
                var rsp = await _articuloServices.Obtener(id);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> Registrar([FromBody] Articulo request)
        {
            try
            {
                var rsp = await _articuloServices.Insertar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al insertar el articulo");
                Console.Write(e.ToString());
            }
            return Ok();

        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Articulo request)
        {
            try
            {
                var rsp = await _articuloServices.Actualizar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar el articulo");
                Console.Write(e.ToString());
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarArticulo = await _articuloServices.Eliminar(id);
            if (eliminarArticulo== false)
            {
                return BadRequest("Error al eliminar el articulo");
            }
            return Ok();
        }

                [HttpGet]
        [Route("ObtenerPorIdTienda/{idTienda}")]
        public async Task<IActionResult> ObtenerPorIdTienda(int idTienda)
        {
            try
            {
                var rsp = await _articuloServices.ObtenerPorIdTienda(idTienda);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok();
        }

        
    }
}
