using Microsoft.EntityFrameworkCore;
using PruebaTecnica.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContexto = new BDContexto())
            {
                dbContexto.Add(pRol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContexto = new BDContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                dbContexto.Update(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContexto = new BDContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                dbContexto.Rol.Remove(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol)
        {
            var rol = new Rol();
            using (var dbContexto = new BDContexto())
            {
                rol = await dbContexto.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            var roles = new List<Rol>();
            using (var dbContexto = new BDContexto())
            {
                roles = await dbContexto.Rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0)
                pQuery = pQuery.Where(r => r.Id == pRol.Id);

            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(r => r.Nombre.Contains(pRol.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();

            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var dbContexto = new BDContexto())
            {
                var select = dbContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
