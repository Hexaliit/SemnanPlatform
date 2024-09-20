using MediatR;
using SemnanCourse.Application.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
