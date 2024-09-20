using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            string[] allowedRoles = { "Admin", "User", "Master" };
            RuleFor(r => r.Role)
                .Must( value => allowedRoles.Contains(value))
                .WithMessage("Role should be Admin,User or Master");
        }
    }
}
