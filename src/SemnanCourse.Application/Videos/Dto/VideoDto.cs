using SemnanCourse.Application.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Dto
{
    public record VideoDto(int Id,string Title, string Path, CourseDto Course);
}
