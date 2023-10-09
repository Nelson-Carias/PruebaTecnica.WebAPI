using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.EntidadesDeNegocio;
using PruebaTecnica.LogicaDeNegocio;
using PruebaTecnica.WebAPI.Auth;
using System.Text.Json;

namespace PruebaTecnica.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private VehiculoBL vehiculoBL = new VehiculoBL();

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Vehiculo>> Get()
        {
            return await vehiculoBL.ObtenerTodosAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Vehiculo> Get(int id)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Id = id;
            return await vehiculoBL.ObtenerPorIdAsync(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Vehiculo vehiculo)
        {
            try
            {
                await vehiculoBL.CrearAsync(vehiculo);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Vehiculo vehiculo)
        {
            if (vehiculo.Id == id)
            {
                await vehiculoBL.ModificarAsync(vehiculo);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Id = id;
                await vehiculoBL.EliminarAsync(vehiculo);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        [AllowAnonymous]
        public async Task<List<Vehiculo>> Buscar([FromBody] object pVehiculo)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strVehiculo = JsonSerializer.Serialize(pVehiculo);
            Vehiculo vehiculo = JsonSerializer.Deserialize<Vehiculo>(strVehiculo, option);
            return await vehiculoBL.BuscarAsync(vehiculo);
        }
    }
}
