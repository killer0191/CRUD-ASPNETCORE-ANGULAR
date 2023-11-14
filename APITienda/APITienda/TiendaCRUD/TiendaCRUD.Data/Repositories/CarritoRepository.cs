using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.Repositories
{
    public class CarritoRepository : IGenericRepository<Carrito>
    {
        private readonly TiendaCrudContext _dbcontext;
        public CarritoRepository(TiendaCrudContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Actualizar(Carrito modelo)
        {
            _dbcontext.Carritos.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Carrito modelo = _dbcontext.Carritos.First(c => c.IdCarrito == id);
            _dbcontext.Carritos.Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Carrito modelo)
        {
            _dbcontext.Carritos.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Carrito> Obtener(int id)
        {
            return await _dbcontext.Carritos.FindAsync(id);
        }

        public async Task<IQueryable<Carrito>> ObtenerTodo()
        {
            IQueryable<Carrito> queryClienteSQL = _dbcontext.Carritos;
            return queryClienteSQL;
        }
    }
}
