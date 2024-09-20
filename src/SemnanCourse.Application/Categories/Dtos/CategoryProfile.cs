using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SemnanCourse.Application.Categories.Commands.Create;
using SemnanCourse.Application.Categories.Commands.Delete;
using SemnanCourse.Application.Categories.Commands.Update;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Application.Categories.Dtos
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<UpdateCategoryCommand, Category>();

            CreateMap<DeleteCategoryCommand,Category>();

            CreateMap<CreateCategoryCommand, Category>();
        }
    }
}
