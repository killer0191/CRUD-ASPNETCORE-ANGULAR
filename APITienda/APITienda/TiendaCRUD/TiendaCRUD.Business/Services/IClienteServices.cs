using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public interface IClienteServices
    {
        Task<bool> Insertar(Cliente modelo);
        Task<bool> Actualizar(Cliente modelo);
        Task<bool> Eliminar(int id);
        Task<Cliente> Obtener(int id);
        Task<IQueryable<Cliente>> ObtenerTodo();
        Task<Cliente> ObtenerPorEmail(string email);
        Task<int> ObteneridPorEmail(string email);
        Task<bool> ValidarPassword(int clienteId, string password);
        Task<Cliente> IniciarSesion(string email, string password);
    }
}
