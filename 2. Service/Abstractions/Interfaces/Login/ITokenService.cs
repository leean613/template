using DTOs.Buffalo;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ITokenService
    {
        Task<JwtTokenResultDto> RequestTokenAsync(LoginResult loginResult);
    }
}
