using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .Length(3, 30);
            RuleFor(u => u.LastName)
                .NotNull()
                .Length(3, 50);

            RuleFor(u => u.Email).NotNull().EmailAddress();
            RuleFor(u => u.Password).NotNull().Length(3, 30);

        }
    }
}
