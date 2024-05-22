using AutoMapper;
using Ecommerce.Core.src.Entity;
using Ecommerce.Core.src.RepoAbstraction;
using Ecommerce.Core.src.ValueObject;
using Ecommerce.Service.src.DTO;
using Ecommerce.Service.src.Service;
using Ecommerce.Service.src.ServiceAbstraction;
using Moq;

namespace Ecommerce.Tests.src.Service
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        private readonly User testAdmin = new User(
                    "admin",
                    "test",
                    UserRole.Admin,
                    "avatarlink",
                    "admin@mail.com",
                    "secure"
                );

        private readonly User testUser = new User(
            "user",
            "test",
            UserRole.User,
            "avatarlink",
            "user@mail.com",
            "secure"
        );

        public UserServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUserRepo.Object, _mockMapper.Object);
        }


    }
}