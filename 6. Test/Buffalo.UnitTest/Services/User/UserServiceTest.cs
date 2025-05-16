using Abstractions.Interfaces.Mail;
using AutoMapper;
using Common.Helpers;
using Common.Runtime.Security;
using DTOs.Buffalo.User;
using Entities.Buffalo;
using EntityFrameworkCore.Contexts;
using EntityFrameworkCore.UnitOfWork;
using Mapper.User;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace ALCare.UnitTest.Controller
{
    public class UserServiceTest
    {
        private readonly Mock<IOptions<AWSInfoOptions>> _awsOptions;
        private readonly Mock<IUnitOfWork<AlcareDbContext>> _unitOfWorkMock;
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly Mock<ISendMailService> _iSendMailService;

        private UserService _userService;
        public UserServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<AlcareDbContext>>();
            _userRepositoryMock = new Mock<IRepository<User>>();
            _awsOptions = new Mock<IOptions<AWSInfoOptions>>();
            _iSendMailService = new Mock<ISendMailService>();
        }

        [Fact]
        public async Task Create_User_Test()
        {
            var fakeUserDTO = new CreateUserDto
            {
                UserId = "abc@gmail.com",
                Password = LoginHelper.EncryptPassword("admin@123"),
                Sex = true,
                PhoneNumber = "012345678",
                DayOfBirth = DateTime.Now,
                UserName = "Binh Nguyen",
            };

            var fakeNewUser = new User
            {
                Id = Guid.NewGuid(),
                IsAlive = false,
                CreatedDate = DateTime.Now,
                Email = "abc@gmail.com",
                Password = LoginHelper.EncryptPassword("admin@123"),
                UserName = "Binh Nguyen",
                DayOfBirth = Convert.ToDateTime("2020/12/09"),
                Gender = true,
                PhoneNumber = "012345678",
                IsActive = true
            };

            // Arrange
            var fakeUserList = new List<User>
            {
               new User
               {
                Id = Guid.NewGuid(),
                IsAlive = false,
                CreatedDate = DateTime.Now,
                Email = "testuser@gmail.com",
                Password = "1",
                UserName = "testuser",
                DayOfBirth = Convert.ToDateTime("2020/12/09"),
                Gender = true,
                PhoneNumber = "111222333",
                IsActive = true
               }
            };

            var mockUserList = fakeUserList.AsQueryable().BuildMock();
            Action fakeInsertAction = () => fakeUserList.Add(fakeNewUser);

            _userRepositoryMock.Setup(u => u.GetAll(false)).Returns(mockUserList.Object).Verifiable();
            _unitOfWorkMock.Setup(x => x.GetRepository<User>()).Returns(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<User>())).Callback(fakeInsertAction).Returns(Task.FromResult(fakeNewUser));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            var mapper = mockMapper.CreateMapper();

            // Act
            _userService = new UserService(_unitOfWorkMock.Object, mapper);
            var result = await _userService.CreateUserAsync(fakeUserDTO);
            // Assert
            Assert.NotNull(fakeUserList.FirstOrDefault(x => x.Email == "abc@gmail.com"));
        }
    }
}
