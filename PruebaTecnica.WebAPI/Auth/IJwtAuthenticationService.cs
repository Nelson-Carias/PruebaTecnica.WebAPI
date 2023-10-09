using PruebaTecnica.EntidadesDeNegocio;

namespace PruebaTecnica.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}
