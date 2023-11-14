using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Data.Repositories
{
    public class ClienteRepository : IGenericRepository<Cliente>
    {
        private readonly TiendaCrudContext _dbcontext;
        public ClienteRepository(TiendaCrudContext context)
        {
                _dbcontext = context;
        }
        public async Task<bool> Actualizar(Cliente modelo)
        {
            _dbcontext.Clientes.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Cliente modelo = _dbcontext.Clientes.First(c => c.IdCliente == id);
            _dbcontext.Clientes.Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Cliente modelo)
        {
            _dbcontext.Clientes.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Cliente> Obtener(int id)
        {
            return await _dbcontext.Clientes.FindAsync(id);
        }

        public async Task<IQueryable<Cliente>> ObtenerTodo()
        {
            IQueryable<Cliente> queryClienteSQL = _dbcontext.Clientes;
            return queryClienteSQL;
        }
    }
}
