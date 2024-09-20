using MediatR;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Commands.UpdateUserBalance
{
    public class UpdateUserBalanceCommandHandler : IRequestHandler<UpdateUserBalanceCommand>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserBalanceCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task Handle(UpdateUserBalanceCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.userId, false, false)
                ?? throw new NotFoundException(UserMessages.NotFound);

            user.Balance += request.Amount;

            await userRepository.UpdateAsync(user);
        }
    }
}
