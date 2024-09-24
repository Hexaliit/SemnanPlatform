using Xunit;
using SemnanCourse.Application.Users.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SemnanCourse.Domain.Constants;
using FluentAssertions;

namespace SemnanCourse.Application.Users.UserService.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>
            {
                new("Id", "1"),
                new(ClaimTypes.Email, "a@a.a"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext() { 
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            // set

            var currentUser = userContext.GetCurrentUser();

            // asset

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Emial.Should().Be("a@a.a");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        }
    }
}