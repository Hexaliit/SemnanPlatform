using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.Update
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.Title).NotNull().Length(3, 50);

            RuleFor(c => c.Description).NotNull().Length(3, 100);

            RuleFor(c => c.Price).GreaterThanOrEqualTo(0);
        }
    }
}
