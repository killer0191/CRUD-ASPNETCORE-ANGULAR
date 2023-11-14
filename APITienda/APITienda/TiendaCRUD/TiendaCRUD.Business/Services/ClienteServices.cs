using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public class ClienteServices : IClienteServices
    {
        private readonly IGenericRepository<Cliente> _clienteRepo;
        public ClienteServices(IGenericRepository<Cliente> clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }
        public async Task<bool> Actualizar(Cliente modelo)
        {
            return await _clienteRepo.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _clienteRepo.Eliminar(id);
        }

        public async Task<bool> Insertar(Cliente modelo)
        {
            return await _clienteRepo.Insertar(modelo);
        }

        public async Task<Cliente> Obtener(int id)
        {
            return await _clienteRepo.Obtener(id);
        }

        public async Task<Cliente> ObtenerPorEmail(string email)
        {
            IQueryable<Cliente> querySQL = await _clienteRepo.ObtenerTodo();
            Cliente cliente = querySQL.FirstOrDefault(c => c.Email == email);
            return cliente;
        }
        public async Task<int> ObteneridPorEmail(string email)
        {
            IQueryable<Cliente> querySQL = await _clienteRepo.ObtenerTodo();
            Cliente cliente = querySQL.FirstOrDefault(c => c.Email == email);
            if (cliente != null)
            {
                return cliente.IdCliente;
            }
            return 0;
        }

        public async Task<bool> ValidarPassword(int clienteId, string password)
        {
            Cliente cliente = await _clienteRepo.Obtener(clienteId);

            if (cliente != null)
            {
                if (cliente.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<Cliente> IniciarSesion(string email, string password)
        {
            Cliente cliente = await ObtenerPorEmail(email);

            if (cliente != null && await ValidarPassword(cliente.IdCliente, password))
            {
                // El inicio de sesión es exitoso, devolver los datos del cliente
                return cliente;
            }

            // El inicio de sesión falló, devolver null o lanzar una excepción según tu preferencia
            return null;
        }
        public async Task<IQueryable<Cliente>> ObtenerTodo()
        {
            return await _clienteRepo.ObtenerTodo();
        }
    }
}
