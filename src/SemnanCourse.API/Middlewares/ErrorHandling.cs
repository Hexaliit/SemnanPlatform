
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using SemnanCourse.Domain.Exceptions;

namespace SemnanCourse.API.Middlewares
{
    public class ErrorHandling : IMiddleware
    {
        private readonly ILogger<ErrorHandling> logger;

        public ErrorHandling(ILogger<ErrorHandling> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
                logger.LogWarning(notFound.Message);
            }
            catch(DependencyExitsException dependency)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(dependency.Message);
                logger.LogWarning(dependency.Message);
            }
            catch(AlreadyExistsException alreadyExists)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(alreadyExists.Message);
                logger.LogWarning(alreadyExists.Message);
            }
            catch (ForbiddenException forbiddenException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
                logger.LogWarning(forbiddenException.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}
