using PruebaTecnica.AccesoADatos;
using PruebaTecnica.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.LogicaDeNegocio
{
    public class VehiculoBL
    {
        public async Task<int> CrearAsync(Vehiculo pVehiculo)
        {
            return await VehiculoDAL.CrearAsync(pVehiculo);
        }

        public async Task<int> ModificarAsync(Vehiculo pVehiculo)
        {
            return await VehiculoDAL.ModificarAsync(pVehiculo);
        }

        public async Task<int> EliminarAsync(Vehiculo pVehiculo)
        {
            return await VehiculoDAL.EliminarAsync(pVehiculo);
        }

        public async Task<Vehiculo> ObtenerPorIdAsync(Vehiculo pVehiculo)
        {
            return await VehiculoDAL.ObtenerPorIdAsync(pVehiculo);
        }

        public async Task<List<Vehiculo>> ObtenerTodosAsync()
        {
            return await VehiculoDAL.ObtenerTodosAsync();
        }

        public async Task<List<Vehiculo>> BuscarAsync(Vehiculo pVehiculo)
        {
            return await VehiculoDAL.BuscarAsync(pVehiculo);
        }
    }
}
