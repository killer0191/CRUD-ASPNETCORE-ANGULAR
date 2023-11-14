using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Entitys;

namespace APITienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoServices _carritoServices;

        public CarritoController(ICarritoServices carritoServices)
        {
            _carritoServices = carritoServices;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<IActionResult> ObtenerTodo()
        {
            try
            {
                var rsp = await _carritoServices.ObtenerTodo();
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
                var rsp = await _carritoServices.Obtener(id);
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
        public async Task<IActionResult> Insertar([FromBody] Carrito request)
        {
            try
            {
                var rsp = await _carritoServices.Insertar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al insertar el carrito");
                Console.Write(e.ToString());
            }
            return Ok();
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Carrito request)
        {
            try
            {
                var rsp = await _carritoServices.Actualizar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar el carrito");
                Console.Write(e.ToString());
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarCarrito = await _carritoServices.Eliminar(id);
            if (eliminarCarrito == false)
            {
                return BadRequest("Error al eliminar el carrito");
            }
            return Ok();
        }

        [HttpGet]
        [Route("ObtenerPorIdCliente/{id:int}")]
        public async Task<IActionResult> ObtenerPorIdCliente(int id)
        {
            try
            {
                var rsp = await _carritoServices.ObtenerPorIdCliente(id);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

[HttpPost]
[Route("Comprar/{idCarrito:int}")]
public async Task<IActionResult> Comprar([FromBody] Compra request, [FromQuery] int idCarrito)
{
    try
    {
        var rsp = await _carritoServices.Comprar(request, idCarrito);
        return Ok(request);
    }
    catch (Exception e)
    {
        Console.WriteLine("Error al realizar la compra");
        Console.Write(e.ToString());
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
                    var rsp = await _carritoServices.OrdenarPorFecha(fechaOrdenar);
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
    }
}
