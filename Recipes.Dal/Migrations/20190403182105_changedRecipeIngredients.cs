using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Dal.Migrations
{
    public partial class changedRecipeIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Measue",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<int>(
                name: "Measure",
                table: "RecipeIngredients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "RecipeIngredients",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Measure",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Measue",
                table: "RecipeIngredients",
                nullable: true);
        }
    }
}
