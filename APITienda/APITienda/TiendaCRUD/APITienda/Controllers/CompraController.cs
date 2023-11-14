using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Entitys;

namespace APITienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraServices _compraServices;

        public CompraController(ICompraServices compraServices)
        {
            _compraServices = compraServices;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<IActionResult> ObtenerTodo()
        {
            try
            {
                var rsp = await _compraServices.ObtenerTodo();
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
                var rsp = await _compraServices.Obtener(id);
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
        public async Task<IActionResult> Insertar([FromBody] Compra request)
        {
            try
            {
                var rsp = await _compraServices.Insertar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al insertar la compra");
                Console.Write(e.ToString());
            }
            return Ok();
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Compra request)
        {
            try
            {
                var rsp = await _compraServices.Actualizar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar la compra");
                Console.Write(e.ToString());
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarCompra = await _compraServices.Eliminar(id);
            if (eliminarCompra == false)
            {
                return BadRequest("Error al eliminar la compra");
            }
            return Ok();
        }

        [HttpGet]
        [Route("ObtenerPorFecha/{fecha}")]
        public async Task<IActionResult> ObtenerPorFecha(string fecha)
        {
            try
            {
                DateTime fechaObtener;
                if (DateTime.TryParse(fecha, out fechaObtener))
                {
                    var rsp = await _compraServices.ObtenerPorFecha(fechaObtener);
                    return Ok(rsp);
                }
                else
                {
                    return BadRequest("Fecha no válida");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [Route("OrdenarPorFecha/{fecha}")]
        public async Task<IActionResult> OrdenarPorFecha(string fecha)
        {
            try
            {
                DateTime fechaOrdenar;
                if (DateTime.TryParse(fecha, out fechaOrdenar))
                {
                    var rsp = await _compraServices.OrdenarPorFecha(fechaOrdenar);
                    return Ok(rsp);
                }
                else
                {
                    return BadRequest("Fecha no válida");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }
        [HttpGet]
        [Route("ObtenerPorIdCliente/{idCliente}")]
        public async Task<IActionResult> ObtenerPorIdCliente(int idCliente)
        {
            try
            {
                var rsp = await _compraServices.ObtenerPorIdCliente(idCliente);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [Route("ObtenerPorIdTienda/{idTienda}")]
        public async Task<IActionResult> ObtenerPorIdTienda(int idTienda)
        {
            try
            {
                var rsp = await _compraServices.ObtenerPorIdTienda(idTienda);
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
