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
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBL = new UsuarioBL();

        private readonly IJwtAuthenticationService authService;

        public UsuarioController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }

        [HttpGet]
        [Route("Lista")]
        [AllowAnonymous]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await usuarioBL.ObtenerTodosAsync();
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        [AllowAnonymous]
        public async Task<Usuario> Get(int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            return await usuarioBL.ObtenerPorIdAsync(usuario);
        }

        [HttpPost]
        [Route("Guardar")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                await usuarioBL.CrearAsync(usuario);
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
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            if (usuario.Id == id)
            {
                await usuarioBL.ModificarAsync(usuario);
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
                Usuario usuario = new Usuario();
                usuario.Id = id;
                await usuarioBL.EliminarAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        [AllowAnonymous]
        public async Task<List<Usuario>> Buscar([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(usuario);
            usuarios.ForEach(s => s.Rol.Usuario = null);
            return usuarios;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            //Codigo para autorizar el usuario por JWT
            Usuario usuario_auth = await usuarioBL.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.Id > 0 && usuario.CorreoElectronico == usuario_auth.CorreoElectronico)
            {
                var token = authService.Authenticate(usuario_auth);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        //[HttpPost("CambiarPassword")]
        //public async Task<ActionResult> CambiarPassword([FromBody] object pUsuario)
        //{
        //    try
        //    {
        //        var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //        string strUsuario = JsonSerializer.Serialize(pUsuario);
        //        Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
        //        await usuarioBL.CambiarPasswordAsync(usuario, usuario.ConfirmPassword_aux);
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
