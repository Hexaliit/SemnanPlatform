﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int? ParentId { get; set; } = null;
        public List<Course> Courses { get; set; }
        public Category Parent { get; set; }
        public List<Category> Children { get; set; }
    }
}
