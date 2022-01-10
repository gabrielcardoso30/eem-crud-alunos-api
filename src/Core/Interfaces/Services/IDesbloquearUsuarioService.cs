using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IDesbloqueioUsuarioService
    {
        Task DesbloquearUsuarios30Minutos();
    }
}
