using AutoMapper;
using MediatR;
using SemnanCourse.Application.Categories.Dtos;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(CategoryMessages.NotFound);

            return mapper.Map<CategoryDto>(category);
        }
    }
}
