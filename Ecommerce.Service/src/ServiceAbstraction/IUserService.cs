using Ecommerce.Service.src.DTO;
using Ecommerce.Core.src.Common;

namespace Ecommerce.Service.src.ServiceAbstraction
{
    public interface IUserService
    {
        Task<UserWithRoleDto> CreateUserAsync(UserCreateDto user);
        Task<UserWithRoleDto> UpdateUserByIdAsync(Guid id, UserUpdateDto user);
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync(QueryOptions options);
        Task<bool> DeleteUserByIdAsync(Guid id);
    }
}
