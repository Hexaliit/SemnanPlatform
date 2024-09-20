using SemnanCourse.Application.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryDto Category { get; set; }
    }
}
