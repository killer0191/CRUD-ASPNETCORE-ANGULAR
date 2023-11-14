using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Entitys;

namespace APITienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public async Task<IActionResult> ObternerTodo()
        {
            try
            {
               var rsp  = await _clienteServices.ObtenerTodo();
                return Ok(rsp);
            }catch (Exception ex)
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
                var rsp = await _clienteServices.Obtener(id);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [Route("ObtenerIdClientePorEmail/{email}")]
        public async Task<IActionResult> ObtenerIdClientePorEmail(string email)
        {
            try
            {
                var idCliente = await _clienteServices.ObteneridPorEmail(email);

                if (idCliente != 0)
                {
                    return Ok(idCliente);
                }
                else
                {
                    return BadRequest("Cliente no encontrado para el correo electrónico proporcionado.");
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
        public async Task<IActionResult> Registrar([FromBody] Cliente request)
        {
            try
            {
                var rsp = await _clienteServices.Insertar(request);
                return Ok(request);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error al registrar el cliente");
                Console.Write(e.ToString());
            }
            return Ok();
            
        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminarCliente = await _clienteServices.Eliminar(id);
            if (eliminarCliente == false)
            {
                return BadRequest("Error al eliminar el cliente");
            }
            return Ok();
        }

        [HttpGet]
        [Route("IniciarSesion/{email}/{clave}")]
        public async Task<IActionResult> IniciarSesion(string email, string clave) 
        {
            var rsp = await _clienteServices.IniciarSesion(email, clave);
            if(rsp == null)
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
