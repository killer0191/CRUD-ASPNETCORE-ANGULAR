using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

namespace TiendaCRUD.Business.Services
{
    public class TiendumServices : ITiendumServices
    {
        private readonly IGenericRepository<Tiendum> _tiendaRepo;
        public TiendumServices(IGenericRepository<Tiendum> tiendaRepo)
        {
            _tiendaRepo = tiendaRepo;
        }

        public async Task<bool> Actualizar(Tiendum modelo)
        {
            return await _tiendaRepo.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _tiendaRepo.Eliminar(id);
        }

        public async Task<bool> Insertar(Tiendum modelo)
        {
            return await _tiendaRepo.Insertar(modelo);
        }

        public async Task<Tiendum> Obtener(int id)
        {
            return await _tiendaRepo.Obtener(id);
        }

        public async Task<Tiendum> ObtenerPorEmail(string email)
        {
            IQueryable<Tiendum> querySQL = await _tiendaRepo.ObtenerTodo();
            Tiendum tienda = querySQL.FirstOrDefault(c => c.Email == email);
            return tienda;
        }
        public async Task<int> ObtenerPoridEmail(string email)
        {
            IQueryable<Tiendum> querySQL = await _tiendaRepo.ObtenerTodo();
            Tiendum tienda = querySQL.FirstOrDefault(c => c.Email == email);
            if (tienda != null)
            {
                return tienda.IdTienda;
            }
            return 0;
        }
        public async Task<bool> ValidarPassword(int tiendaId, string password)
        {
            Tiendum tienda = await _tiendaRepo.Obtener(tiendaId);

            if (tienda != null)
            {
                if (tienda.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<Tiendum> IniciarSesion(string email, string password)
        {
            Tiendum tienda = await ObtenerPorEmail(email);

            if (tienda != null && await ValidarPassword(tienda.IdTienda, password))
            {
                return tienda;
            }
            return null;
        }
        public async Task<IQueryable<Tiendum>> ObtenerTodo()
        {
            return await _tiendaRepo.ObtenerTodo();
        }
    }
}
