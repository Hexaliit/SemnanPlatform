using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using SemnanCourse.Application.Courses.Commands.Create;
using SemnanCourse.Application.Courses.Commands.Update;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Dto
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course,CourseDto>();

            CreateMap<UpdateCourseCommand, Course>();
        }
    }
}
