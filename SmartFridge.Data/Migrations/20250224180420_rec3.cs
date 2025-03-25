using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFridge.Data.Migrations
{
    public partial class rec3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissedPro",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsedPro",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissedPro",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UsedPro",
                table: "Recipes");
        }
    }
}
