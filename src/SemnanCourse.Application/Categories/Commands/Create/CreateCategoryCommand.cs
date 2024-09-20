using MediatR;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
