using HousingRepairsOnlineApi.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingRepairsOnlineApi.Migrations
{
    public partial class Repairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "repair",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    postcode = table.Column<string>(type: "text", nullable: true),
                    sor = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<RepairAddress>(type: "jsonb", nullable: true),
                    location = table.Column<RepairLocation>(type: "jsonb", nullable: true),
                    problem = table.Column<RepairProblem>(type: "jsonb", nullable: true),
                    issue = table.Column<RepairIssue>(type: "jsonb", nullable: true),
                    contact_person_number = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<RepairDescription>(type: "jsonb", nullable: true),
                    contact_details = table.Column<RepairContactDetails>(type: "jsonb", nullable: true),
                    time = table.Column<RepairAvailability>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repair", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "repair");
        }
    }
}
