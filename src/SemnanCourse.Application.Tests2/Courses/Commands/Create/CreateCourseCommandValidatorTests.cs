using Xunit;
using SemnanCourse.Application.Courses.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using FluentValidation.TestHelper;

namespace SemnanCourse.Application.Courses.Commands.Create.Tests
{
    public class CreateCourseCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.FileName).Returns("image.jpg");
            fileMock.Setup(x => x.Length).Returns(100);
            var Command = new CreateCourseCommand()
            {
                Title = "new Title",
                Description = "New Description",
                Price = 10000,
                Avatar = fileMock.Object,
            };

            var validator = new CreateCourseCommandValidator();
            // Act
            var result = validator.TestValidate(Command);
            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var Command = new CreateCourseCommand()
            {
                Title = "n",
                Description = "N",
                Price = -1,
                Avatar = null,
            };

            var validator = new CreateCourseCommandValidator();
            // set
            var result = validator.TestValidate(Command);
            // asset
            result.ShouldHaveValidationErrorFor(c => c.Title);
            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.Avatar);
            result.ShouldHaveValidationErrorFor(c => c.Price);
        }
    }
}