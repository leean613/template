using DTOs.Buffalo;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginDto dto);
    }
}
