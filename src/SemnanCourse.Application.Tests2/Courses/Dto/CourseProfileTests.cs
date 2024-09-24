using Xunit;
using SemnanCourse.Application.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SemnanCourse.Domain.Entities;
using FluentAssertions;
using SemnanCourse.Application.Categories.Dtos;

namespace SemnanCourse.Application.Courses.Dto.Tests
{
    public class CourseProfileTests
    {
        [Fact()]
        public void CreateMap_ForCourseToCourseDto_MapCorrectly()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CourseProfile>();
                cfg.AddProfile<CategoryProfile>();
            });

            var mapper = configuration.CreateMapper();

            var course = new Course()
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                Price = 10000,
                Avatar = "image",
                CreatedAt = DateTime.Now,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Title",
                    ParentId = null
                }
            };

            // Act
            var courseDto = mapper.Map<CourseDto>(course);

            // Assert
            courseDto.Should().NotBeNull();
            courseDto.Id.Should().Be(course.Id);
            courseDto.Title.Should().Be(course.Title);
            courseDto.Description.Should().Be(course.Description);
            courseDto.Price.Should().Be(course.Price);
            courseDto.Avatar.Should().Be(course.Avatar);
            courseDto.CreatedAt.Should().Be(course.CreatedAt);
            
        }
    }
}