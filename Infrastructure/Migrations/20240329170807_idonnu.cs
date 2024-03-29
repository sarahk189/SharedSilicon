using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class idonnu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseAuthorEntityCourseEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseAuthorEntityCourseEntity",
                columns: table => new
                {
                    Authorsid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAuthorEntityCourseEntity", x => new { x.Authorsid, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseAuthorEntityCourseEntity_CoursesAuthor_Authorsid",
                        column: x => x.Authorsid,
                        principalTable: "CoursesAuthor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAuthorEntityCourseEntity_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAuthorEntityCourseEntity_CoursesId",
                table: "CourseAuthorEntityCourseEntity",
                column: "CoursesId");
        }
    }
}
