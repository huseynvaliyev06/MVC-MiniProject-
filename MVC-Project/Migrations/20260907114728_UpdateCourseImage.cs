using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseImg",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "CourseImages");

            migrationBuilder.AddColumn<int>(
                name: "AppImageId",
                table: "CourseImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CourseImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppImageId",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CourseImages");

            migrationBuilder.AddColumn<string>(
                name: "CourseImg",
                table: "CourseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "CourseImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
