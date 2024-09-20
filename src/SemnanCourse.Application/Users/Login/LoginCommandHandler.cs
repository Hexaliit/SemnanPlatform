using MediatR;
using SemnanCourse.Domain.Exceptions;
using SemnanCourse.Domain.Messages;
using SemnanCourse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtTokenGenerator tokenGenerator;

        public LoginCommandHandler(IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator)
        {
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Authenticate(request.Email, request.Password)
                ?? throw new InvalidCredentialException(UserMessages.NotFound);

            var token = tokenGenerator.Generate(user);
            return new LoginResponse(token);
             
        }
    }
}
