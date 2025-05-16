using Abstractions.Interfaces;
using Buffalo.Controllers;
using Common.Runtime.Security;
using DTOs.Buffalo;
using Entities.Buffalo;
using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ALCare.UnitTest.Controller
{
    public class LoginServiceTest
    {
        private readonly Mock<IOptions<AWSInfoOptions>> _mockAwsOptions;
        private readonly Mock<IUnitOfWork<AlcareDbContext>> _mockUnitOfWork;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<ILoginService> _mockLoginService;
        private readonly Mock<ITokenService> _mockTokenService;
        private List<LoginDto> _loginSource = new List<LoginDto>
        {
            new LoginDto()
            {
                UserName = "admin",
                Password = "1",
            } 
        };
        public LoginServiceTest()
        {
            _mockLoginService = new Mock<ILoginService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockUnitOfWork = new Mock<IUnitOfWork<AlcareDbContext>>();
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockAwsOptions = new Mock<IOptions<AWSInfoOptions>>();
        }
        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            var controller = new LoginController( _mockTokenService.Object, _mockLoginService.Object);
            Assert.NotNull(controller);
        }
    }

}
