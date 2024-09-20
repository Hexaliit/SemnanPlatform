using AutoMapper;
using MediatR;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand,Category>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if(request.ParentId != null)
            {
                if(!await categoryRepository.ExistAny(c => c.Id  == request.ParentId && c.ParentId == null))
                {
                    throw new NotFoundException(CategoryMessages.InvalidParent);
                }
            }
            var category = mapper.Map<Category>(request);
            
            category.Courses = null;
            category.Parent = null;
            category.Children = null;
            
            category = await categoryRepository.CreateAsync(category);

            return category;
        }
    }
}
