using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public RegisterCommandHandler(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }
        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if(await userRepository.ExitsAny(u => u.Email == request.Email))
            {
                throw new AlreadyExistsException(UserMessages.EmailExists);
            }

            var user = mapper.Map<User>(request);

            var role = await roleRepository.GetByNameAsync("User");

            user.Roles.Add(role!);

            var createdUser = await userRepository.CreateAsync(user);
        }
    }
}
