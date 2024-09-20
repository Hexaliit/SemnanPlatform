using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Application.Courses.Commands.AddCourseForUser;
using SemnanCourse.Application.Courses.Commands.Create;
using SemnanCourse.Application.Courses.Commands.Delete;
using SemnanCourse.Application.Courses.Commands.Update;
using SemnanCourse.Application.Courses.Dto;
using SemnanCourse.Application.Courses.Queries.GetAllCourses;
using SemnanCourse.Application.Courses.Queries.GetCourseById;
using SemnanCourse.Domain.Constants;
using SemnanCourse.Domain.Models;

namespace SemnanCourse.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        ///     Retrives a list of courses by parameters.
        /// </summary>
        /// <param name="query">Request params.</param>
        /// <returns>List of courses.</returns>
        /// <response code="200">Returns a list of courses with pagination metadata.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<PagedResult<CourseDto>>> Get([FromQuery] GetAllCoursesQuery query)
        {
            var courses = await mediator.Send(query);
            return Ok(courses);
        }
        /// <summary>
        ///     Creates a new course.
        /// </summary>
        /// <param name="command">Params fo creating a new course.</param>
        /// <response code="400">If the params are invalid.</response>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unAuthorized (is not Admin or Master).</response>
        /// <response code="409">If the course with given params already exists.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Master)]
        public async Task<IActionResult> Create([FromForm] CreateCourseCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        ///     Updates a course by it's given Id.
        /// </summary>
        /// <param name="command">Params for updating course.</param>
        /// <param name="id">The Id of the course that should update</param>
        /// <response code="400">If the params are invalid.</response>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unAuthorized (is not Admin or Master).</response>
        /// <response code="404">If the course woth given Id not found.</response>
        /// <response code="409">If the course with given params already exists.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Master)]
        public async Task<IActionResult> Update([FromForm] UpdateCourseCommand command, int id)
        {
            command.CourseId = id;
            await mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        ///     Retrives a course bi it's Id.
        /// </summary>
        /// <param name="id">The Id of the course.</param>
        /// <returns>The requested course specified by it's Id.</returns>
        /// <response code="404">If the course woth given Id not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var query = new GetCourseByIdQuery(id);
            var courseDto = await mediator.Send(query);
            return Ok(courseDto);  
        }


        /// <summary>
        ///  Deletes a specified course by it's Id by admin or owner of the course
        /// </summary>
        /// <param name="id">The Specified Id of the course</param>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unAuthorized (is not Admin or Master).</response>
        /// <response code="404">If the course with given Id not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize(Roles =UserRoles.Admin + "," + UserRoles.Master)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteCourseCommand(id));
            return NoContent();
        }

        /// <summary>
        ///     Add specified course to the authenticated user
        /// </summary>
        /// <param name="id">The Id of the course.</param>
        /// <response code="400">If the user is has not enough balance</response>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="404">If the course with given Id not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{id}/add")]
        [Authorize(Roles =UserRoles.User)]
        public async Task<IActionResult> AddToUser(int id)
        {
            var command = new AddCourseForUserCommand()
            {
                CourseId = id
            };
            await mediator.Send(command);
            return Ok();
        }
    }
}
 