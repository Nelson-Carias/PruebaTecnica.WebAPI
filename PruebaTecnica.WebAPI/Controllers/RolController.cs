using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.EntidadesDeNegocio;
using PruebaTecnica.LogicaDeNegocio;
using System.Text.Json;

namespace PruebaTecnica.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private RolBL rolBL = new RolBL();

        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<IEnumerable<Rol>> Get()
        {
            return await rolBL.ObtenerTodosAsync();
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        [AllowAnonymous]
        public async Task<Rol> Get(int id)
        {
            Rol rol = new Rol();
            rol.Id = id;
            return await rolBL.ObtenerPorIdAsync(rol);
        }

        [HttpPost]
        [Route("Guardar")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] Rol rol)
        {
            try
            {
                await rolBL.CrearAsync(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Editar/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Put(int id, [FromBody] Rol rol)
        {
            if (rol.Id == id)
            {
                await rolBL.ModificarAsync(rol);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Rol rol = new Rol();
                rol.Id = id;
                await rolBL.EliminarAsync(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        [AllowAnonymous]
        public async Task<List<Rol>> Buscar([FromBody] object pRol)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strRol = JsonSerializer.Serialize(pRol);
            Rol rol = JsonSerializer.Deserialize<Rol>(strRol, option);
            return await rolBL.BuscarAsync(rol);
        }
    }
}
