using Microsoft.EntityFrameworkCore;
using PruebaTecnica.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.AccesoADatos
{
    public class VehiculoDAL
    {
        public static async Task<int> CrearAsync(Vehiculo pVehiculo)
        {
            int result = 0;
            using (var dbContexto = new BDContexto())
            {
                dbContexto.Add(pVehiculo);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Vehiculo pVehiculo)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var vehiculo = await bdContexto.Vehiculo.FirstOrDefaultAsync(d => d.Id == pVehiculo.Id);
                vehiculo.Nombre = pVehiculo.Nombre;
                vehiculo.Año = pVehiculo.Año;
                vehiculo.Descripcion = pVehiculo.Descripcion;
                vehiculo.Precio = pVehiculo.Precio;
                vehiculo.Imagen = pVehiculo.Imagen;

                bdContexto.Update(vehiculo);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Vehiculo pVehiculo)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var director = await bdContexto.Vehiculo.FirstOrDefaultAsync(d => d.Id == pVehiculo.Id);
                bdContexto.Vehiculo.Remove(director);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Vehiculo> ObtenerPorIdAsync(Vehiculo pVehiculo)
        {
            var vehiculo = new Vehiculo();
            using (var bdContexto = new BDContexto())
            {
                vehiculo = await bdContexto.Vehiculo.FirstOrDefaultAsync(d => d.Id == pVehiculo.Id);
            }
            return vehiculo;
        }

        public static async Task<List<Vehiculo>> ObtenerTodosAsync()
        {
            var vehiculos = new List<Vehiculo>();
            using (var bdContecto = new BDContexto())
            {
                vehiculos = await bdContecto.Vehiculo.ToListAsync();
            }
            return vehiculos;
        }

        internal static IQueryable<Vehiculo> QuerySelect(IQueryable<Vehiculo> pQuery, Vehiculo pVehiculo)
        {
            if (pVehiculo.Id > 0)
                pQuery = pQuery.Where(d => d.Id == pVehiculo.Id);

            if (!string.IsNullOrWhiteSpace(pVehiculo.Nombre))
                pQuery = pQuery.Where(d => d.Nombre.Contains(pVehiculo.Nombre));

            if (!string.IsNullOrWhiteSpace(pVehiculo.Año))
                pQuery = pQuery.Where(d => d.Año.Contains(pVehiculo.Año));

            if (!string.IsNullOrWhiteSpace(pVehiculo.Descripcion))
                pQuery = pQuery.Where(d => d.Descripcion.Contains(pVehiculo.Descripcion));

            pQuery = pQuery.OrderByDescending(d => d.Id).AsQueryable();
            if (pVehiculo.Top_Aux > 0)
                pQuery = pQuery.Take(pVehiculo.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Vehiculo>> BuscarAsync(Vehiculo pVehiculo)
        {
            var vehiculos = new List<Vehiculo>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Vehiculo.AsQueryable();
                select = QuerySelect(select, pVehiculo);
                vehiculos = await select.ToListAsync();
            }
            return vehiculos;
        }
    }
}
