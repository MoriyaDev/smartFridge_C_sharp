using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFridge.Data.Migrations
{
    public partial class cnt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "missedIngredientCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usedIngredientCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "missedIngredientCount",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "usedIngredientCount",
                table: "Recipes");
        }
    }
}
