using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class yup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAuthorEntityCourseEntity_CoursesAuthor_AuthorsId",
                table: "CourseAuthorEntityCourseEntity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CoursesAuthor",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "CourseAuthorEntityCourseEntity",
                newName: "Authorsid");

            migrationBuilder.AddColumn<Guid>(
                name: "Authorid",
                table: "CoursesDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesDetails_Authorid",
                table: "CoursesDetails",
                column: "Authorid");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAuthorEntityCourseEntity_CoursesAuthor_Authorsid",
                table: "CourseAuthorEntityCourseEntity",
                column: "Authorsid",
                principalTable: "CoursesAuthor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDetails_CoursesAuthor_Authorid",
                table: "CoursesDetails",
                column: "Authorid",
                principalTable: "CoursesAuthor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAuthorEntityCourseEntity_CoursesAuthor_Authorsid",
                table: "CourseAuthorEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDetails_CoursesAuthor_Authorid",
                table: "CoursesDetails");

            migrationBuilder.DropIndex(
                name: "IX_CoursesDetails_Authorid",
                table: "CoursesDetails");

            migrationBuilder.DropColumn(
                name: "Authorid",
                table: "CoursesDetails");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CoursesAuthor",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Authorsid",
                table: "CourseAuthorEntityCourseEntity",
                newName: "AuthorsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAuthorEntityCourseEntity_CoursesAuthor_AuthorsId",
                table: "CourseAuthorEntityCourseEntity",
                column: "AuthorsId",
                principalTable: "CoursesAuthor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
