using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Avatar { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<User> Users { get; set; } = new();
        public List<Video> Videos { get; set; } = new();
        public Category Category { get; set; } = new();
        public User User { get; set; } = new();


    }
}
