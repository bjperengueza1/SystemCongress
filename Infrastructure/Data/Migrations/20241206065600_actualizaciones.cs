using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class actualizaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
