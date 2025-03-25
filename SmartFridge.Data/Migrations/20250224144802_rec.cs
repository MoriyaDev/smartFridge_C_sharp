using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFridge.Data.Migrations
{
    public partial class rec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usedIngredientCount",
                table: "Recipes",
                newName: "UsedIngredientCount");

            migrationBuilder.RenameColumn(
                name: "missedIngredientCount",
                table: "Recipes",
                newName: "MissedIngredientCount");

            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "Recipes",
                newName: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsedIngredientCount",
                table: "Recipes",
                newName: "usedIngredientCount");

            migrationBuilder.RenameColumn(
                name: "MissedIngredientCount",
                table: "Recipes",
                newName: "missedIngredientCount");

            migrationBuilder.RenameColumn(
                name: "Products",
                table: "Recipes",
                newName: "ExternalId");
        }
    }
}
