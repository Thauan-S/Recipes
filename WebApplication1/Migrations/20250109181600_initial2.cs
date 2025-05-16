using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "86eb5e41-dcf7-4bf5-8b86-5fd9d55611db" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86eb5e41-dcf7-4bf5-8b86-5fd9d55611db");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8ef5b78e-d6a3-4397-b369-82258d3eb432", 0, "591d2191-2c33-450e-82d4-343665ed1cf6", "freetrained@freetrained.com", true, false, null, "FREETRAINED@FREETRAINED.COM", "FREETRAINED@FREETRAINED.COM", "AQAAAAIAAYagAAAAECHd9hJCwN4Ish6bR4eZ5KpHFYT1K66K3b+xy2SjY0345TACKGnm3684s9wxhPFY2w==", "1234567890", true, "92c78cbf-398b-45b3-9394-e8843386a828", false, "freetrained@freetrained.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "8ef5b78e-d6a3-4397-b369-82258d3eb432" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "8ef5b78e-d6a3-4397-b369-82258d3eb432" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8ef5b78e-d6a3-4397-b369-82258d3eb432");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "86eb5e41-dcf7-4bf5-8b86-5fd9d55611db", 0, "52b6fd46-48f2-4e1a-94a5-28e837b9e837", "freetrained@freetrained.com", true, false, null, "FREETRAINED@FREETRAINED.COM", "FREETRAINED@FREETRAINED.COM", "AQAAAAIAAYagAAAAEEoyPcXHkRuzFdNV6THcHJtO0AuuQ32iHA/l3zpPo3nm6h7JZFZmYA+MoJi175vuQg==", "1234567890", true, "68a9fcb7-4bf6-4911-8744-2ba653ad860c", false, "freetrained@freetrained.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "86eb5e41-dcf7-4bf5-8b86-5fd9d55611db" });
        }
    }
}
