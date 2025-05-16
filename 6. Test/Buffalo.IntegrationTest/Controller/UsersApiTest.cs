using DTOs.Buffalo.User;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ALCare.IntegrationTest.Controller
{
    public class UsersApiTest : TestFixture
    {
        [Fact]
        public async Task Create_User_Return_CreatedUser()
        {
            // Act
            var userDTO = new CreateUserDto
            {
                UserId = "usertest",
                Password = "1",
                UserName = "User For Test",
                System = "MANAGEMENT",
                DayOfBirth = Convert.ToDateTime("2020/12/09"),
                Sex = true,
                PhoneNumber = "0932491054"
            };
            var contents = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/buffalo/user", contents);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
