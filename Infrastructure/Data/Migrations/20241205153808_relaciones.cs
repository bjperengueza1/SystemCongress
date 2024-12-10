using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exposures",
                columns: table => new
                {
                    ExposureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusExposure = table.Column<int>(type: "int", nullable: false),
                    ResearchLine = table.Column<int>(type: "int", nullable: false),
                    CongressId = table.Column<int>(type: "int", nullable: false),
                    CongressoCongressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exposures", x => x.ExposureId);
                    table.ForeignKey(
                        name: "FK_Exposures_Congresses_CongressoCongressId",
                        column: x => x.CongressoCongressId,
                        principalTable: "Congresses",
                        principalColumn: "CongressId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Exposures_CongressoCongressId",
                table: "Exposures",
                column: "CongressoCongressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exposures");
        }
    }
}
