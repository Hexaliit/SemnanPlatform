using Xunit;
using SemnanCourse.Application.Users.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace SemnanCourse.Application.Users.Register.Tests
{
    public class RegisterCommandValidatorTests
    {
        [Fact()]
        public void RegisterCommandValidator_ForValidCommand_ShouldNotHaveErrors()
        {
            // Arrange
            var command = new RegisterCommand()
            { 
                FirstName = "abcd",
                LastName = "efgh",
                Email = "a@a.a",
                Password = "password"
            };

            var validator = new RegisterCommandValidator();
            // Act
            var result = validator.TestValidate(command);
            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void RegisterCommandValidator_ForInvalidCommand_ShouldHaveErrors()
        {
            // Arrange
            var command = new RegisterCommand()
            {
                FirstName = "ab",
                LastName = "e",
                Email = "email",
                Password = null
            };

            var validator = new RegisterCommandValidator();
            // Act
            var result = validator.TestValidate(command);
            // Assert
            result.ShouldHaveValidationErrorFor(c => c.FirstName);
            result.ShouldHaveValidationErrorFor(c => c.LastName);
            result.ShouldHaveValidationErrorFor(c => c.Email);
            result.ShouldHaveValidationErrorFor(c => c.Password);
        }
    }
}