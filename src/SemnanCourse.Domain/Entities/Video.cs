using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int CourseId { get; set; }
        public bool ShowOnDemo { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Course? Course { get; set; } = new();
    }
}
