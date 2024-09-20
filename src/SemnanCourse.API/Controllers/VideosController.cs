using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Application.Categories.Queries.GetCategoryById;
using SemnanCourse.Application.Videos.Commands.Create;
using SemnanCourse.Application.Videos.Commands.Delete;
using SemnanCourse.Application.Videos.Commands.Update;
using SemnanCourse.Application.Videos.Commands.UpdateVideoFile;
using SemnanCourse.Application.Videos.Dto;
using SemnanCourse.Application.Videos.Queries.GetAllVideos;
using SemnanCourse.Application.Videos.Queries.GetVideoById;
using SemnanCourse.Domain.Constants;

namespace SemnanCourse.API.Controllers
{
    [Route("api/courses/{courseId}/videos")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin +","+ UserRoles.Master)]
    public class VideosController : ControllerBase
    {
        private readonly IMediator mediator;

        public VideosController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        ///     Retrives all videos for a specific course Id.
        /// </summary>
        /// <param name="courseId">The Id of course for getting videos.</param>
        /// <response code="404">The Id of the specific course was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<VideosForCourseDto>>> Get(int courseId)
        {
            var videos = await mediator.Send(new GetAllVideosQuery(courseId));
            return Ok(videos);
        }

        /// <summary>
        ///     Retrives the specific video for the given course id and video id.
        /// </summary>
        /// <param name="courseId">The Id of the course.</param>
        /// <param name="VideoId">The Id of the video</param>
        /// <response code="200">Returns the video for specific Id.</response>
        /// <response code="401">Unauthenticated user for getting a not free course that has not show on demo.</response>
        /// <response code="403">Unauthorized user that did not bought a not free course that has not show on demo video.</response>
        /// <response code="404">The given Id for course or video was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{videoId}")]
        [AllowAnonymous]
        public async Task<ActionResult<VideoDto>> GetById(int courseId, int VideoId)
        {
            var command = new GetVideoByIdQuery()
            {
                VideoId = VideoId,
                CourseId = courseId
            };
            var videoDto = await mediator.Send(command);
            return Ok(videoDto);
        }

        /// <summary>
        ///  Creates a video for a specific course Id.
        /// </summary>
        /// <param name="courseId">The Specifies course Id.</param>
        /// <param name="command">Data for creating a new course.</param>
        /// <response code="401">Unauthenticated user.</response>
        /// <response code="403">Unauthorized user (Admin or Master).</response>
        /// <response code="404">The given Id for course or video was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> Post(int courseId, [FromForm] CreateVideoCommand command)
        {
            command.CourseId = courseId;
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates a video with a specific Id.
        /// </summary>
        /// <param name="courseId">The course id.</param>
        /// <param name="videoId">The video Id.</param>
        /// <param name="command">Data to update video.</param>
        /// <response code="400">Invalid video arguments</response>
        /// <response code="401">Unauthenticated user.</response>
        /// <response code="403">Unauthorized user (Admin or Master).</response>
        /// <response code="404">The given Id for course or video was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{videoId}")]
        public async Task<IActionResult> Update(int courseId,int videoId, UpdateVideoCommand command)
        {
            command.CourseId = courseId;
            command.VideoId = videoId;
            await mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        ///     Delets a specific video with an Id.
        /// </summary>
        /// <param name="videoId">The video Id.</param>
        /// <param name="courseId">The Course Id.</param>
        /// <response code="401">Unauthenticated user.</response>
        /// <response code="403">Unauthorized user (Admin or Master).</response>
        /// <response code="404">The given Id for course or video was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{videoId}")]
        public async Task<IActionResult> Delete(int videoId, int courseId)
        {
            var command = new DeleteVideoCommand()
            {
                VideoId = videoId,
                CourseId = courseId
            };
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates a video file for a specific video Id.
        /// </summary>
        /// <param name="courseId">The specific course Id.</param>
        /// <param name="videoId">The specific video Id.</param>
        /// <param name="command">Video File to upload.</param>
        /// <response code="401">Unauthenticated user.</response>
        /// <response code="403">Unauthorized user (Admin or Master).</response>
        /// <response code="404">The given Id for course or video was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{videoId}/update")]
        public async Task<IActionResult> UpdateVideo(int courseId, int videoId, UpdateVideoFileCommand command)
        {
            command.VideoId = videoId;
            command.CourseId = courseId;
            await mediator.Send(command);
            return NoContent();
        }
    }

}
