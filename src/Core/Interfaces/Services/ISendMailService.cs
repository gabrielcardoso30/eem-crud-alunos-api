using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMsSendMail
    {
        Task<bool> SendMailAsync(
            string to, 
            string subject,
            string message, 
            string entity, 
            string entityId, 
            int priority,
            byte[]? file = null,
            string? fileMimetype = null,
            string? fileName = null
        );
    }

}