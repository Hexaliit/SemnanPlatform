using Xunit;
using SemnanCourse.Application.Users.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Domain.Entities;
using FluentAssertions;

namespace SemnanCourse.Application.Users.Login.Tests
{
    public class LoginCommandHandlerTests
    {
        [Fact()]
        public void LoginCommand_ForValidCommand_ShouldReturnJWT()
        {
            // Arrange
            var user = new User() { Id = 1, Email = "a@a.a", Roles = new List<Role> {new (){Id = 1, Name = "Admin" } } };
            var userrepositoryMock = new Mock<IUserRepository>();
            userrepositoryMock
                .Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);
            var tokenMock = new Mock<IJwtTokenGenerator>();
            tokenMock.Setup(j => j.Generate(user)).Returns("JWTTOKEN");

            var command = new LoginCommand();

            var commandhandler = new LoginCommandHandler(userrepositoryMock.Object, tokenMock.Object);
            // Act
            var result = commandhandler.Handle(command, CancellationToken.None);
            // Assert
            result.GetType().Should().Be(typeof(Task<LoginResponse>));
        }
    }
}