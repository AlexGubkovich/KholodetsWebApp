using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeEatKholodets.Migrations
{
    public partial class FavoriteRecipeUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipeId",
                table: "UserRecipes");

            migrationBuilder.RenameColumn(
                name: "FavoriteRecipeId",
                table: "UserRecipes",
                newName: "FavoriteRecipesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipesId",
                table: "UserRecipes",
                column: "FavoriteRecipesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipesId",
                table: "UserRecipes");

            migrationBuilder.RenameColumn(
                name: "FavoriteRecipesId",
                table: "UserRecipes",
                newName: "FavoriteRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipeId",
                table: "UserRecipes",
                column: "FavoriteRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
