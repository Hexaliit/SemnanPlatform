using SemnanCourse.Infrastructure.Extensions;
using SemnanCourse.Application.Extensions;
using Serilog;
using Microsoft.Identity.Client;
using SemnanCourse.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandling>();

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
// we use our middleware as the first middleware in the pipeline
app.UseMiddleware<ErrorHandling>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
