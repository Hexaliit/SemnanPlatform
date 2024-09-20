using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Application.Users.Login;
using SemnanCourse.Application.Users.Register;

namespace SemnanCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        ///     Registes a new user.
        /// </summary>
        /// <param name="command">Params to register a new user.</param>
        /// <response code="400">If the params are missing or invalid.</response>
        /// <response code="409">If specific email is already exists in user' table.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        ///     Login a user.
        /// </summary>
        /// <param name="command">Params to login user.</param>
        /// <returns>New JWT Token.</returns>
        /// <response code="400">Invalid Credentials.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginCommand command)
        {
            var token = await mediator.Send(command);
            return Ok(token);
        }
    }
}
