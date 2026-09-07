using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project.Migrations
{
    /// <inheritdoc />
    public partial class cREATEDDDDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // AuthorImage və Tag artıq DB-dədir, yalnız rename/drop əməliyyatları lazımdır

            // IsFeature silinir
            migrationBuilder.DropColumn(
                name: "IsFeature",
                table: "Courses");

            // IsNew → IsFeatured
            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "Courses",
                newName: "IsFeatured");

            // Title → Name
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorImage",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "IsFeatured",
                table: "Courses",
                newName: "IsNew");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeature",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
