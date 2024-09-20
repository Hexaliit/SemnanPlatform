﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemnanCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShoOnDemoFieldToVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnDemo",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnDemo",
                table: "Videos");
        }
    }
}
