using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingRepairsOnlineApi.Migrations
{
    public partial class CreatedAtAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "repair",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "repair");
        }
    }
}
