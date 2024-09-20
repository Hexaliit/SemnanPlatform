using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Role> Roles { get; set; } = new();
        public List<Course> Courses { get; } = new();
        public List<Course> UserCourses { get; } = new();


    }
}
