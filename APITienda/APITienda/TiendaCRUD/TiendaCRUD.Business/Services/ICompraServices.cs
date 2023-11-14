using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public interface ICompraServices
    {
        Task<bool> Insertar(Compra modelo);
        Task<bool> Actualizar(Compra modelo);
        Task<bool> Eliminar(int id);
        Task<Compra> Obtener(int id);
        Task<IQueryable<Compra>> ObtenerTodo();
        Task<Compra> ObtenerPorFecha(DateTime fecha);
        Task<IQueryable<Compra>> OrdenarPorFecha(DateTime fecha);
         Task<IQueryable<Compra>> ObtenerPorIdCliente(int idCliente);
         Task<IQueryable<Compra>> ObtenerPorIdTienda(int idTienda);

    }
}
