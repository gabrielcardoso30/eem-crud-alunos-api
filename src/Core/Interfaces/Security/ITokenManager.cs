using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        Task<string> GetActiveAsync(string id, string tokenType);
        Task ActivateAsync(string id, string token);
    }
}