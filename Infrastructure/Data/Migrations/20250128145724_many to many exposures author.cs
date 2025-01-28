using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class manytomanyexposuresauthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Exposures_ExposureId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ExposureId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ExposureId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "ExposureAuthor",
                columns: table => new
                {
                    ExposureAuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ExposureId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExposureAuthor", x => x.ExposureAuthorId);
                    table.ForeignKey(
                        name: "FK_ExposureAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExposureAuthor_Exposures_ExposureId",
                        column: x => x.ExposureId,
                        principalTable: "Exposures",
                        principalColumn: "ExposureId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExposureAuthor_AuthorId",
                table: "ExposureAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExposureAuthor_ExposureId",
                table: "ExposureAuthor",
                column: "ExposureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExposureAuthor");

            migrationBuilder.AddColumn<int>(
                name: "ExposureId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ExposureId",
                table: "Authors",
                column: "ExposureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Exposures_ExposureId",
                table: "Authors",
                column: "ExposureId",
                principalTable: "Exposures",
                principalColumn: "ExposureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
