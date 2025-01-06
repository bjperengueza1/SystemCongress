using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addrelationexposuretocongress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CongressId",
                table: "Exposures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exposures_CongressId",
                table: "Exposures",
                column: "CongressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exposures_Congresses_CongressId",
                table: "Exposures",
                column: "CongressId",
                principalTable: "Congresses",
                principalColumn: "CongressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exposures_Congresses_CongressId",
                table: "Exposures");

            migrationBuilder.DropIndex(
                name: "IX_Exposures_CongressId",
                table: "Exposures");

            migrationBuilder.DropColumn(
                name: "CongressId",
                table: "Exposures");
        }
    }
}
