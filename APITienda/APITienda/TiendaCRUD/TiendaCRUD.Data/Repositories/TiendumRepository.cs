using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.Repositories
{
    public class TiendumRepository : IGenericRepository<Tiendum>
    {
        private readonly TiendaCrudContext _dbcontext;
        public TiendumRepository(TiendaCrudContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Tiendum modelo)
        {
            _dbcontext.Tienda.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Tiendum modelo = _dbcontext.Tienda.First(c => c.IdTienda == id);
            _dbcontext.Tienda.Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Tiendum modelo)
        {
            _dbcontext.Tienda.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Tiendum> Obtener(int id)
        {
            return await _dbcontext.Tienda.FindAsync(id);
        }

        public async Task<IQueryable<Tiendum>> ObtenerTodo()
        {
            IQueryable<Tiendum> queryClienteSQL = _dbcontext.Tienda;
            return queryClienteSQL;
        }
    }
}
