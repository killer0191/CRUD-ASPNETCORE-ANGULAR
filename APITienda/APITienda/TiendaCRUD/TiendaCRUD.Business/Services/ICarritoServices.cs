using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public interface ICarritoServices
    {
        Task<bool> Insertar(Carrito modelo);
        Task<bool> Actualizar(Carrito modelo);
        Task<bool> Eliminar(int id);
        Task<Carrito> Obtener(int id);
        Task<IQueryable<Carrito>> ObtenerPorIdCliente(int idCliente);
        Task<IQueryable<Carrito>> ObtenerTodo();
        Task<IQueryable<Carrito>> OrdenarPorFecha(DateTime fecha);
        Task<bool> Comprar(Compra modelo, int IdCarrito);

    }
}
