using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Application.Users.Commands.UpdateUserBalance;
using SemnanCourse.Application.Users.Commands.UpdateUserRole;
using SemnanCourse.Application.Users.Dto;
using SemnanCourse.Application.Users.GetUserProfile;
using SemnanCourse.Domain.Constants;

namespace SemnanCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public UsersController(IMapper mapper,
            IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }
        /// <summary>
        ///     Adds amount to the specific user.
        /// </summary>
        /// <param name="userId">The user Id to add amount.</param>
        /// <param name="command">Param for adding amount</param>
        /// <response code="400">If the params are missing or invalid</response>
        /// <response code="401">If the user is unauthenticated</response>
        /// <response code="403">If the user is unuthorized(not Admin).</response>
        /// <response code="404">If the user with specific Id was nor found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{userId}/addbalance")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateBalance(int userId, UpdateUserBalanceCommand command)
        {
            command.userId = userId;
            await mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        ///     Updates user roles.
        /// </summary>
        /// <param name="userId">The user Id to update role.</param>
        /// <param name="command">Param for updating role.</param>
        /// <response code="400">If the params are missing or invalid</response>
        /// <response code="401">If the user is unauthenticated</response>
        /// <response code="403">If the user is unuthorized(not Admin).</response>
        /// <response code="404">If the user with specific Id was nor found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{userId}/updaterole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateRole(int userId, UpdateUserRoleCommand command)
        {
            command.UserId = userId;
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        ///     Retrives authenticated user's profile.
        /// </summary>
        /// <returns>The authenticated user's profile.</returns>
        /// <response code="401">If the user is unauthenticated</response>
        /// <response code="403">If the user is unuthorized(not User).</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("profile")]
        [Authorize(Roles =UserRoles.User)]
        public async Task<ActionResult<UserDto>> Get()
        {
            var command = new GetUserProfileQuery();
            var userDto = await mediator.Send(command);
            return Ok(userDto);
        }
    }
}
