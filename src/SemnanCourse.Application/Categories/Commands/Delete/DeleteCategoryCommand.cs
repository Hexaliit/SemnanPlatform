using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
        public int Id { get;}
    }
}
