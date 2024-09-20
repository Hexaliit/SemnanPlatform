using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public UpdateUserRoleCommandHandler(IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }
        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, true, false)
                ?? throw new NotFoundException(UserMessages.NotFound);

            var role = await roleRepository.GetByNameAsync(request.Role)
                ?? throw new NotFoundException(RoleMessages.NotFound);

            foreach(var userRole in user.Roles)
            {
                user.Roles.Remove(userRole);
            }
            user.Roles.Add(role);
            await userRepository.UpdateAsync(user);
        }
    }
}
