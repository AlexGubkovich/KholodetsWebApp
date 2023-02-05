using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeEatKholodets.Migrations
{
    public partial class RecipePhotoRequiredAndViewcoun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfViews",
                table: "Recipes",
                newName: "ViewCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ViewCount",
                table: "Recipes",
                newName: "NumberOfViews");
        }
    }
}
