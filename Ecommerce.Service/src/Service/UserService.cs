using AutoMapper;
using Ecommerce.Core.src.Common;
using Ecommerce.Core.src.Entity;
using Ecommerce.Core.src.RepoAbstraction;
using Ecommerce.Core.src.ValueObject;
using Ecommerce.Service.src.DTO;
using Ecommerce.Service.src.ServiceAbstraction;
using Ecommerce.Service.src.Validation;

namespace Ecommerce.Service.src.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userDto)
        {
            // Optional: validate the user DTO if you have a validation method
            // UserValidation.ValidateUserCreateDto(userDto);

            // Map the UserCreateDto to a User entity
            var user = _mapper.Map<User>(userDto);
            // Ensure CreatedAt is set to UTC
            user.CreatedAt = DateTimeOffset.UtcNow;

            // Check if a user with the given email already exists
            var isUserExistWithEmail = await _userRepo.UserExistsByEmailAsync(user.Email);

            if (isUserExistWithEmail)
            {
                throw AppException.UserCredentialErrorEmailAlreadyExist(user.Email);
            }

            // Create the user asynchronously
            var createdUser = await _userRepo.CreateUserAsync(user);

            // Map the created User entity to UserReadDto and return it
            return _mapper.Map<UserReadDto>(createdUser);
        }


        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var existingUser = await _userRepo.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return await _userRepo.DeleteUserByIdAsync(id);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync(QueryOptions options)
        {
            var users = await _userRepo.GetAllUsersAsync(options);

            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetUserByIdAsync(id) ?? throw AppException.UserNotFound($"User with ID {id} not found.");
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> UpdateUserByIdAsync(Guid id, UserUpdateDto userDto)
        {
            var existingUser = await _userRepo.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                throw AppException.UserNotFound($"User with ID {id} not found.");
            }

            UserValidation.ValidateUserUpdateDto(userDto);

            _mapper.Map(userDto, existingUser);

            return await _userRepo.UpdateUserByIdAsync(existingUser);
        }
    }
}
