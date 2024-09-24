using Xunit;
using SemnanCourse.Application.Videos.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using SemnanCourse.Domain.Interfaces.Services;
using FluentAssertions;

namespace SemnanCourse.Application.Videos.Commands.Create.Tests
{
    public class CreateVideoCommandHandlerTests
    {
        [Fact()]
        public async Task CreteVidoCommand_ForValidCommand_ShouldRunWithoutError()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();


            var courseRepoMock = new Mock<ICourseRepository>();
            var course = new Course() { Id = 1 };
            courseRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(),false, false, false)).ReturnsAsync(course);

            var authorizeMock = new Mock<IApplicationAuthorizationService>();
            authorizeMock.Setup(a => a.Authorize(course)).Returns(true);
            
            var vidoeServiceMock = new Mock<IVideoServiceRepository>();
            var path = "NewVidoeFile";
            vidoeServiceMock.Setup(x => x.CreateVideo(It.IsAny<IFormFile>())).ReturnsAsync(path);

            var command = new CreateVideoCommand();
            var video = new Video();
            mapperMock.Setup(m=>m.Map<Video>(command)).Returns(video);

            var videoRepositoryMock = new Mock<IVideoRepository>();
            videoRepositoryMock.Setup(v => v.CreateAsync(It.IsAny<Video>()));

            var commandHandler = new CreateVideoCommandHandler(courseRepoMock.Object,
                videoRepositoryMock.Object,
                vidoeServiceMock.Object,
                mapperMock.Object,
                authorizeMock.Object);
            // Act
            await commandHandler.Handle(command, CancellationToken.None);
            // Assert
            video.Path.Should().Be("NewVidoeFile");
            videoRepositoryMock.Verify(c => c.CreateAsync(video), Times.Once);
        }
    }
}