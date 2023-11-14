using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public interface ITiendumServices
    {
        Task<bool> Insertar(Tiendum modelo);
        Task<bool> Actualizar(Tiendum modelo);
        Task<bool> Eliminar(int id);
        Task<Tiendum> Obtener(int id);
        Task<IQueryable<Tiendum>> ObtenerTodo();
        Task<Tiendum> ObtenerPorEmail(string email);
        Task<int> ObtenerPoridEmail(string email);
        Task<bool> ValidarPassword(int tiendaId, string password);
        Task<Tiendum> IniciarSesion(string email, string password);
    }
}
