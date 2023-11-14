using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.Repositories
{
    public class CompraRepository : IGenericRepository<Compra>
    {
        private readonly TiendaCrudContext _dbcontext;
    public CompraRepository(TiendaCrudContext context)
    {
        _dbcontext = context;
    }

        public async Task<bool> Actualizar(Compra modelo)
        {
            _dbcontext.Compras.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Compra modelo = _dbcontext.Compras.First(c => c.IdCompra == id);
            _dbcontext.Compras.Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Compra modelo)
        {
            _dbcontext.Compras.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Compra> Obtener(int id)
        {
            return await _dbcontext.Compras.FindAsync(id);
        }

        public async Task<IQueryable<Compra>> ObtenerTodo()
        {
            IQueryable<Compra> queryClienteSQL = _dbcontext.Compras;
            return queryClienteSQL;
        }
    }
}
