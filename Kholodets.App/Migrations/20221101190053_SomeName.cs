using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeEatKholodets.Migrations
{
    public partial class SomeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfLikes",
                table: "Recipes",
                newName: "NumberOfViews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfViews",
                table: "Recipes",
                newName: "NumberOfLikes");
        }
    }
}
