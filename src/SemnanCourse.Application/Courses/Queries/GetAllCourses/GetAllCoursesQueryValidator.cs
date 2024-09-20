using FluentValidation;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQueryValidator : AbstractValidator<GetAllCoursesQuery>
    {
        private int[] allowedPageSize = { 5, 10, 15, 30 };
        private string[] allowedSortColumn = { nameof(Course.Title), nameof(Course.Price), "Date" };
        
        public GetAllCoursesQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Must(value => allowedPageSize.Contains(value)).WithMessage("Page size must be 5, 10, 15, 30");

            RuleFor(r => r.SortBy).Must(value => allowedSortColumn.Contains(value)).WithMessage("Allowed sort columns are Title, Price and Date");
        }
    }
}
