using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public class CompraServices : ICompraServices
    {
        private readonly IGenericRepository<Compra> _compraRepo;

        public CompraServices(IGenericRepository<Compra> compraRepo)
        {
            _compraRepo = compraRepo;
        }

        public async Task<bool> Actualizar(Compra modelo)
        {
            return await _compraRepo.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _compraRepo.Eliminar(id);
        }

        public async Task<bool> Insertar(Compra modelo)
        {
            return await _compraRepo.Insertar(modelo);
        }

        public async Task<Compra> Obtener(int id)
        {
            return await _compraRepo.Obtener(id);
        }

        public async Task<Compra> ObtenerPorFecha(DateTime fecha)
        {
            IQueryable<Compra> querySQL = await _compraRepo.ObtenerTodo();
            Compra compra = querySQL.FirstOrDefault(c => c.Fecha == fecha);
            return compra;
        }

        public async Task<IQueryable<Compra>> ObtenerTodo()
        {
            return await _compraRepo.ObtenerTodo();
        }

        public async Task<IQueryable<Compra>> OrdenarPorFecha(DateTime fecha)
        {
            var comprasOrdenadas = await _compraRepo.ObtenerTodo();
            comprasOrdenadas = comprasOrdenadas.OrderBy(c => c.Fecha);

            return comprasOrdenadas;
        }
public async Task<IQueryable<Compra>> ObtenerPorIdCliente(int idCliente)
{
    IQueryable<Compra> querySQL = await _compraRepo.ObtenerTodo();
    IQueryable<Compra> compras = querySQL.Where(c => c.IdCliente == idCliente);
    return compras;
}

public async Task<IQueryable<Compra>> ObtenerPorIdTienda(int idTienda)
{
    IQueryable<Compra> querySQL = await _compraRepo.ObtenerTodo();
    IQueryable<Compra> compras = querySQL.Where(c => c.IdTienda == idTienda);
    return compras;
}
    }
}
