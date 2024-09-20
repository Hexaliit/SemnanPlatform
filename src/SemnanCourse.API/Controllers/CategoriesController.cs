using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemnanCourse.Application.Categories.Commands.Create;
using SemnanCourse.Application.Categories.Commands.Delete;
using SemnanCourse.Application.Categories.Commands.Update;
using SemnanCourse.Application.Categories.Dtos;
using SemnanCourse.Application.Categories.Queries.GetAllCategories;
using SemnanCourse.Application.Categories.Queries.GetCategoryById;
using SemnanCourse.Domain.Constants;

namespace SemnanCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CategoriesController(IMediator mediator,IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <summary>
        ///  Retrives list of categories.
        /// </summary>
        /// <response code="200">/returns a list of categories</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categoriesDto = await mediator.Send(new GetAllCategoriesQuery());
            return Ok(categoriesDto);
        }

        /// <summary>
        ///     Gets a specific category by it's Id
        /// </summary>
        /// <param name="id">Categoty's Id</param>
        /// <response code="404">If the category was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var categoryDto = await mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(categoryDto);
        }

        /// <summary>
        ///     Creates a category by Admin
        /// </summary>
        /// <param name="command">Data for creating new category.</param>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unauthorized (not an Admin)</response>
        /// <response code="409">If a category with same name exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryCommand command)
        {
            var category = await mediator.Send(command);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        /// <summary>
        ///     Updating a specified category by it's Id.
        /// </summary>
        /// <param name="command">The update data for city.</param>
        /// <param name="id">Id of the category.</param>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unauthorized (not an Admin)</response>
        /// <response code="404">If the specified category id not found.</response>
        /// <response code="409">If a category with same name exists.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(UpdateCategoryCommand command, int id)
        {
            command.CategoryId = id;
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        ///     Delets a specified category by it's Id.
        /// </summary>
        /// <param name="id">The Id of Category.</param>
        /// <response code="401">If the user is unauthenticated.</response>
        /// <response code="403">If the user is unauthorized (not an Admin)</response>
        /// <response code="404">If the specified category id not found.</response>
        /// <response code="409">If there are courses or subcategories for given Id.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteCategoryCommand(id));
            return NoContent(); 
        }
    }
}
