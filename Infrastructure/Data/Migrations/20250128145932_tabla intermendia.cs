using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablaintermendia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExposureAuthor_Authors_AuthorId",
                table: "ExposureAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_ExposureAuthor_Exposures_ExposureId",
                table: "ExposureAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExposureAuthor",
                table: "ExposureAuthor");

            migrationBuilder.RenameTable(
                name: "ExposureAuthor",
                newName: "ExposureAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_ExposureAuthor_ExposureId",
                table: "ExposureAuthors",
                newName: "IX_ExposureAuthors_ExposureId");

            migrationBuilder.RenameIndex(
                name: "IX_ExposureAuthor_AuthorId",
                table: "ExposureAuthors",
                newName: "IX_ExposureAuthors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExposureAuthors",
                table: "ExposureAuthors",
                column: "ExposureAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureAuthors_Authors_AuthorId",
                table: "ExposureAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureAuthors_Exposures_ExposureId",
                table: "ExposureAuthors",
                column: "ExposureId",
                principalTable: "Exposures",
                principalColumn: "ExposureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExposureAuthors_Authors_AuthorId",
                table: "ExposureAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_ExposureAuthors_Exposures_ExposureId",
                table: "ExposureAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExposureAuthors",
                table: "ExposureAuthors");

            migrationBuilder.RenameTable(
                name: "ExposureAuthors",
                newName: "ExposureAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_ExposureAuthors_ExposureId",
                table: "ExposureAuthor",
                newName: "IX_ExposureAuthor_ExposureId");

            migrationBuilder.RenameIndex(
                name: "IX_ExposureAuthors_AuthorId",
                table: "ExposureAuthor",
                newName: "IX_ExposureAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExposureAuthor",
                table: "ExposureAuthor",
                column: "ExposureAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureAuthor_Authors_AuthorId",
                table: "ExposureAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExposureAuthor_Exposures_ExposureId",
                table: "ExposureAuthor",
                column: "ExposureId",
                principalTable: "Exposures",
                principalColumn: "ExposureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
