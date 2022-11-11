using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeEatKholodets.Migrations
{
    public partial class FavoriteRecipeUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeUser_AspNetUsers_UsersId",
                table: "RecipeUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeUser_Recipes_FavoriteRecipeId",
                table: "RecipeUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeUser",
                table: "RecipeUser");

            migrationBuilder.RenameTable(
                name: "RecipeUser",
                newName: "UserRecipes");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeUser_UsersId",
                table: "UserRecipes",
                newName: "IX_UserRecipes_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes",
                columns: new[] { "FavoriteRecipeId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_AspNetUsers_UsersId",
                table: "UserRecipes",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipeId",
                table: "UserRecipes",
                column: "FavoriteRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_AspNetUsers_UsersId",
                table: "UserRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecipes_Recipes_FavoriteRecipeId",
                table: "UserRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes");

            migrationBuilder.RenameTable(
                name: "UserRecipes",
                newName: "RecipeUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserRecipes_UsersId",
                table: "RecipeUser",
                newName: "IX_RecipeUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeUser",
                table: "RecipeUser",
                columns: new[] { "FavoriteRecipeId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeUser_AspNetUsers_UsersId",
                table: "RecipeUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeUser_Recipes_FavoriteRecipeId",
                table: "RecipeUser",
                column: "FavoriteRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
