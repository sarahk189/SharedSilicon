using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesno2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesAuthor_AuthorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Courses",
                newName: "CourseAuthorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                newName: "IX_Courses_CourseAuthorId1");

            migrationBuilder.AddColumn<int>(
                name: "CourseAuthorId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesAuthor_CourseAuthorId1",
                table: "Courses",
                column: "CourseAuthorId1",
                principalTable: "CoursesAuthor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesAuthor_CourseAuthorId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseAuthorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseAuthorId1",
                table: "Courses",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseAuthorId1",
                table: "Courses",
                newName: "IX_Courses_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesAuthor_AuthorId",
                table: "Courses",
                column: "CourseAuthorId",
                principalTable: "CoursesAuthor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
