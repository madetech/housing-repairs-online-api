using HousingRepairsOnlineApi.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingRepairsOnlineApi.Migrations
{
    public partial class RemovePII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact_details",
                table: "repair");

            migrationBuilder.DropColumn(
                name: "contact_person_number",
                table: "repair");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<RepairContactDetails>(
                name: "contact_details",
                table: "repair",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_person_number",
                table: "repair",
                type: "text",
                nullable: true);
        }
    }
}
