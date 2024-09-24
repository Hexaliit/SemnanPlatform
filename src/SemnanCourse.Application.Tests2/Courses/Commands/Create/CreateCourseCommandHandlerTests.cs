using Xunit;
using SemnanCourse.Application.Courses.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using SemnanCourse.Application.Users.UserService;
using FluentAssertions;

namespace SemnanCourse.Application.Courses.Commands.Create.Tests
{
    public class CreateCourseCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_CreateCourseSuccessfully()
        {
            // arrang
            var loggerMock = new Mock<ILogger<CreateCourseCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateCourseCommand()
            {
                CategoryId = 12
            };
            var course = new Course();

            mapperMock.Setup(m => m.Map<Course>(command)).Returns(course);

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync(new Category() { Id = 12 });

            var imageMock = new Mock<IFormFile>();
            imageMock.Setup(c => c.FileName).Returns("image.jpg");
            imageMock.Setup(c => c.Length).Returns(800);


            var imageRepositoryMock = new Mock<IImageRepository>();
            imageRepositoryMock
                .Setup(repo => repo.CreateImage(imageMock.Object))
                .ReturnsAsync("images");

            var courseMock = new Mock<ICourseRepository>();
            courseMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Course>()));

            var userMock = new Mock<IUserRepository>();
            userMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User()
                {
                    FirstName = "ali", LastName = "afshar", Id = 1, Email = "a@a.a",
                    Balance = 123, CreatedAt = DateTime.Now, Password = "123123", Roles = new List<Role> {new Role(){ Id = 1, Name = "Admin"} }
                });

            var currentUserMock = new Mock<IUserContext>();
            currentUserMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser() { Id = "5", Emial = "a@a.c", Roles =  new List<string> { } });


            var commandHandler = new CreateCourseCommandHandler(courseMock.Object,
                mapperMock.Object,
                categoryRepositoryMock.Object,
                userMock.Object,
                currentUserMock.Object,
                imageRepositoryMock.Object
                );

            // act
            await commandHandler.Handle(command, CancellationToken.None);

            // asset
            course.UserId.Should().Be(Int32.Parse("5"));
            courseMock.Verify(c => c.CreateAsync(course), Times.Once);
            //course.Avatar.Should().Be("images");
        }

    }
}