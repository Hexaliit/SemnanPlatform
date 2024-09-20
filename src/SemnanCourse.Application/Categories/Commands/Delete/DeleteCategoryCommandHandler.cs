using AutoMapper;
using MediatR;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id,true)
                ?? throw new NotFoundException(CategoryMessages.NotFound);

            if(await categoryRepository.ExistAny(c => c.ParentId == request.Id))
            {
                throw new DependencyExitsException(CategoryMessages.NotFound);
            }

            mapper.Map(request, category);
            
            await categoryRepository.DeleteAsync(category);

            // for deleting cascade 
            /*if (category.Children.Count > 0)
            {
                // creates a copy of a list 
                foreach (var child in category.Children.ToList())
                {
                    await categoryRepository.DeleteAsync(child);
                }
            }
            */
        }
    }
}
