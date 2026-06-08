using Microsoft.EntityFrameworkCore.Migrations;

/// <summary>
/// Adds unique index on Users.Email and index on Users.RefreshToken
/// Run: dotnet ef database update
/// </summary>
#nullable disable
namespace School_Management_System.Infrastructure.Migrations
{
    public partial class AddEmailAndIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name:    "IX_Users_Email",
                table:   "Users",
                column:  "Email",
                unique:  true);

            migrationBuilder.CreateIndex(
                name:   "IX_Users_RefreshToken",
                table:  "Users",
                column: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Users_Email",        table: "Users");
            migrationBuilder.DropIndex(name: "IX_Users_RefreshToken", table: "Users");
        }
    }
}
