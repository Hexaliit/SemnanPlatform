using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(CategoryMessages.NotFound);

            if(request.ParentId != null)
            {
                if(!await categoryRepository.ExistAny(c => c.Id == request.ParentId && c.ParentId == null))
                {
                    throw new NotFoundException(CategoryMessages.InvalidParent);
                }
            }

            mapper.Map(request,category);
            category = await categoryRepository.UpdateAsync(category);
        }
    }
}
