using DTOs.Buffalo.User;
using System.Threading.Tasks;

namespace Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
    }
}
