using Globitel.Domain.DTO;
using Globitel.Domain.Models;
using System.Threading.Tasks;

namespace Globitel.Service.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> Login(LoginDTO model);
    }
}
