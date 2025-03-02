using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentCourses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "StudentCourses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
