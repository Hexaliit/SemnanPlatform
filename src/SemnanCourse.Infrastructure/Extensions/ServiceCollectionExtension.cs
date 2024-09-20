using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SemnanCourse.Domain.Entities;
using SemnanCourse.Domain.Repositories;
using SemnanCourse.Infrastructure.Persistence.Contextes;
using SemnanCourse.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SemCourse");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtToken:Issuer"],
                    ValidAudience = configuration["JwtToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:Key"]!))
                }
            );
            
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleRepository,  RoleRepository>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<ICourseRepository,CourseRepository>();

            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddScoped<IVideoRepository, VideoRepository>();

            services.AddScoped<IVideoServiceRepository, VideoServiceRepository>();
        }
    }
}
