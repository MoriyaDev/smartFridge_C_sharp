using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFridge.Data.Migrations
{
    public partial class recipetabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Recipes",
                newName: "Image");

            migrationBuilder.AddColumn<int>(
                name: "CriticalProductCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticalProductCount",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Recipes",
                newName: "image");
        }
    }
}
