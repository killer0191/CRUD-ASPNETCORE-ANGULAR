using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.Repositories
{
    public class ArticuloRepository : IGenericRepository<Articulo>
    {
        private readonly TiendaCrudContext _dbcontext;
        public ArticuloRepository(TiendaCrudContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Actualizar(Articulo modelo)
        {
            _dbcontext.Articulos.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Articulo modelo = _dbcontext.Articulos.First(c => c.IdArticulo == id);
            _dbcontext.Articulos.Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Articulo modelo)
        {
            _dbcontext.Articulos.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Articulo> Obtener(int id)
        {
            return await _dbcontext.Articulos.FindAsync(id);
        }

        public async Task<IQueryable<Articulo>> ObtenerTodo()
        {
            IQueryable<Articulo> querySQL = _dbcontext.Articulos;
            return querySQL;
        }
    }
}
