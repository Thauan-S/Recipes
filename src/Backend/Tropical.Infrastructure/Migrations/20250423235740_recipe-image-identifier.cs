using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tropical.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recipeimageidentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageIdentifier",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageIdentifier",
                table: "Recipes");
        }
    }
}
