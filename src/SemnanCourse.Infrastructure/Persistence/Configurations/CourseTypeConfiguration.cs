using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Persistence.Configurations
{
    public class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId);

            builder.HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .UsingEntity(
                "CourseUser",
                c => c.HasOne(typeof(User)).WithMany().HasForeignKey("UserId")
                .HasPrincipalKey(nameof(User.Id)).OnDelete(DeleteBehavior.NoAction),
                u => u.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseId")
                .HasPrincipalKey(nameof(Course.Id)).OnDelete(DeleteBehavior.NoAction),
                cu => cu.HasKey("CourseId", "UserId"));

            builder.HasOne(c => c.User)
                .WithMany(u => u.UserCourses)
                .HasForeignKey(c => c.UserId);
        }
    }
}
