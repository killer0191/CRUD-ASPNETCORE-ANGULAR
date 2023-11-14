using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public class ArticuloServices : IArticuloServices
    {
        private readonly IGenericRepository<Articulo> _articuloRepo;
        public ArticuloServices(IGenericRepository<Articulo> articuloRepo)
        {
            _articuloRepo = articuloRepo;
        }

        public async Task<bool> Actualizar(Articulo modelo)
        {
            return await _articuloRepo.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _articuloRepo.Eliminar(id);
        }

        public async Task<bool> Insertar(Articulo modelo)
        {
            return await _articuloRepo.Insertar(modelo);
        }

        public async Task<Articulo> Obtener(int id)
        {
            return await _articuloRepo.Obtener(id);
        }

        public async Task<IQueryable<Articulo>> ObtenerTodo()
        {
            return await _articuloRepo.ObtenerTodo();
        }
        public async Task<IQueryable<Articulo>> ObtenerPorIdTienda(int idTienda)
{
    IQueryable<Articulo> querySQL = await _articuloRepo.ObtenerTodo();
    IQueryable<Articulo> articulos = querySQL.Where(c => c.IdTienda == idTienda);
    return articulos;
}
    }
}
