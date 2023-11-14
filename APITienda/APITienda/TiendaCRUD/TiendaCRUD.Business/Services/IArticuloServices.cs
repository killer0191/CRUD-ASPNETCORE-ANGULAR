using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public interface IArticuloServices
    {
        Task<bool> Insertar(Articulo modelo);
        Task<bool> Actualizar(Articulo modelo);
        Task<bool> Eliminar(int id);
        Task<Articulo> Obtener(int id);
        Task<IQueryable<Articulo>> ObtenerTodo();
        Task<IQueryable<Articulo>> ObtenerPorIdTienda(int idTienda);
    }
}
