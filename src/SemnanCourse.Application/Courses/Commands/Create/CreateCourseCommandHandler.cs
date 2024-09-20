using AutoMapper;
using MediatR;
using SemnanCourse.Application.Users.UserService;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Courses.Commands.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserContext userContext;
        private readonly IImageRepository imageRepository;

        public CreateCourseCommandHandler(ICourseRepository courseRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IUserContext userContext,
            IImageRepository imageRepository)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
            this.userContext = userContext;
            this.imageRepository = imageRepository;
        }
        public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            if( !await categoryRepository.ExistAny(c => c.Id == request.CategoryId))
            {
                throw new NotFoundException(CategoryMessages.NotFound);
            }

            var fileName = await imageRepository.CreateImage(request.Avatar);

            var course = new Course()
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Avatar = fileName,
                CategoryId = request.CategoryId,
                UserId = Int32.Parse(userContext.GetCurrentUser()!.Id),
                User = null,
                Users = null,
                Category = null,
                Videos = null
            };

            await courseRepository.CreateAsync(course);
 
        }
    }
}
