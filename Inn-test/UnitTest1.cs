using Async_Inn.Controllers;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.Security.Claims;

namespace Inn_test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Register_User_As_District_Manager()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JwtTokenService>(null, null);

            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "District Manager") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));

            var controller = new UsersController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };

            var registerDto = new RegisterDTO
            {
                Username = "TestUser",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                password = "P@ssw0rd",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            var expectedResult = new UserDTO
            {
                ID = "UserId",
                Username = registerDto.Username,
                Token = "MockedToken",
                Roles = new List<string> { "Agent" } // Adjust the roles as needed
            };

            userMock.Setup(u => u.Register(It.IsAny<RegisterDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.Username, userDto.Username);
            Assert.Equal(expectedResult.Roles, userDto.Roles);

        }
    }
}