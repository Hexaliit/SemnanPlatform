using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemnanCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Infrastructure.Persistence.Configurations
{
    public class VideoTypeConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(v => v.Id);

            builder.HasOne(v => v.Course)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CourseId);
        }
    }
}
