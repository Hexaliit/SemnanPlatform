using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
