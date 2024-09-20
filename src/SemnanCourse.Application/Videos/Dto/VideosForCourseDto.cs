using SemnanCourse.Application.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Videos.Dto
{
    public record VideosForCourseDto(int Id,
                                     string Title,
                                     bool ShowOnDemo,
                                     DateTime CreatedAt);
}
