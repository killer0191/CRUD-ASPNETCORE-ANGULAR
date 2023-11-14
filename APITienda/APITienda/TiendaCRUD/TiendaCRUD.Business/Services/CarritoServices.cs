using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public class CarritoServices : ICarritoServices
    {
        private readonly IGenericRepository<Carrito> _carritoRepo;
        private readonly IGenericRepository<Compra> _compraRepo;
        private readonly IGenericRepository<Articulo> _articuloRepo;
        public CarritoServices(IGenericRepository<Carrito> carritoRepo, IGenericRepository<Compra> compraRepo, IGenericRepository<Articulo> articuloRepo)
        {
            _carritoRepo = carritoRepo;
            _compraRepo = compraRepo;
            _articuloRepo = articuloRepo;
        }

        public async Task<bool> Actualizar(Carrito modelo)
        {
            return await _carritoRepo.Actualizar(modelo);
        }

        public async Task<bool> Comprar(Compra modelo, int IdCarrito){
    bool compraExitosa = await _compraRepo.Insertar(modelo);
    if (compraExitosa)
    {
        await _carritoRepo.Eliminar(IdCarrito);
    }
    return compraExitosa;
}

/*public async Task<bool> ComprarTodo(int idCliente)
{
    try
    {
        IQueryable<Carrito> carritoItems = await ObtenerPorIdCliente(idCliente);
        foreach (var carritoItem in carritoItems)
        {
            Compra modeloCompra = new Compra
            {
                Fecha = DateTime.Now,
                //Total = carritoItem.Precio,
                IdArticulo = carritoItem.IdArticulo,
                Total =  await  _articuloRepo.Obtener(carritoItem.IdArticulo),
                IdCliente = idCliente,
                IdTienda = carritoItem.IdTienda,
            };
            bool compraExitosa = await Comprar(modeloCompra, carritoItem.IdCarrito);
            if (!compraExitosa)
            {
                Console.WriteLine($"Error al comprar el artículo {carritoItem.IdArticulo}");
            }
        }
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al comprar todo: {ex.Message}");
        return false;
    }
}
*/
        public async Task<bool> Eliminar(int id)
        {
            return await _carritoRepo.Eliminar(id);
        }

        public async Task<bool> Insertar(Carrito modelo)
        {
            return await _carritoRepo.Insertar(modelo);
        }

        public async Task<Carrito> Obtener(int id)
        {
            return await _carritoRepo.Obtener(id);
        }

   public async Task<IQueryable<Carrito>> ObtenerPorIdCliente(int idCliente)
{
    IQueryable<Carrito> querySQL = await _carritoRepo.ObtenerTodo();
    IQueryable<Carrito> carrito = querySQL.Where(c => c.IdCliente == idCliente);
    return carrito;
}

        public async Task<IQueryable<Carrito>> ObtenerTodo()
        {
            return await _carritoRepo.ObtenerTodo();
        }

        public async Task<IQueryable<Carrito>> OrdenarPorFecha(DateTime fecha)
        {
            var comprasOrdenadas = await _carritoRepo.ObtenerTodo();
            comprasOrdenadas = comprasOrdenadas.OrderBy(c => c.Fecha);

            return comprasOrdenadas;
        }
    }
}
