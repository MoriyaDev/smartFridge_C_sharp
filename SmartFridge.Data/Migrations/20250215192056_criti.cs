using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFridge.Data.Migrations
{
    public partial class criti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_RecipeId1",
                table: "Products",
                column: "RecipeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Recipes_RecipeId1",
                table: "Products",
                column: "RecipeId1",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Recipes_RecipeId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_RecipeId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RecipeId1",
                table: "Products");
        }
    }
}
