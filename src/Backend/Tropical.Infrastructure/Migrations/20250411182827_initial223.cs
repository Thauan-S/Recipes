using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tropical.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoockingTime",
                table: "Recipes",
                newName: "CookingTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CookingTime",
                table: "Recipes",
                newName: "CoockingTime");
        }
    }
}
