using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Entitys;

namespace APITienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendumController : ControllerBase
    {
        private readonly ITiendumServices _tiendaServices;

        public TiendumController(ITiendumServices tiendaServices)
        {
            _tiendaServices = tiendaServices;
        }
        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<IActionResult> ObternerTodo()
        {
            try
            {
                var rsp = await _tiendaServices.ObtenerTodo();
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
                var rsp = await _tiendaServices.Obtener(id);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [Route("ObtenerIdTiendaPorEmail/{email}")]
        public async Task<IActionResult> ObtenerIdTiendaPorEmail(string email)
        {
            try
            {
                var idTienda = await _tiendaServices.ObtenerPoridEmail(email);

                if (idTienda != 0)
                {
                    return Ok(idTienda);
                }
                else
                {
                    return BadRequest("Tienda no encontrada para el correo electrónico proporcionado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Tiendum request)
        {
            try
            {
                var rsp = await _tiendaServices.Insertar(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al registrar la tienda");
                Console.Write(e.ToString());
            }
            return Ok();

        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarTienda = await _tiendaServices.Eliminar(id);
            if (eliminarTienda == false)
            {
                return BadRequest("Error al eliminar la tienda");
            }
            return Ok();
        }

        [HttpGet]
        [Route("IniciarSesion/{email}/{clave}")]
        public async Task<IActionResult> IniciarSesion(string email, string clave)
        {
            var rsp = await _tiendaServices.IniciarSesion(email, clave);
            if (rsp == null)
            {
                Console.WriteLine("Email y/o contraseña incorrectas");
                return BadRequest();
            }
            else
            {
                Console.WriteLine("Ingreso correcto");
                return Ok(rsp);
            }
        }
    }
}
