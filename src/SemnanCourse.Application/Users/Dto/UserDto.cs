using SemnanCourse.Application.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Dto
{
    public record UserDto(string FirstName,
        string LastName, string Email,decimal Balance, IEnumerable<CourseDto> Courses);
}
