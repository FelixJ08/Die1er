using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Die1Er_Projektarbeit.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Benutzer",
                newName: "Vorname");

            migrationBuilder.AddColumn<string>(
                name: "Nachname",
                table: "Benutzer",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Benutzer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nachname",
                table: "Benutzer");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Benutzer");

            migrationBuilder.RenameColumn(
                name: "Vorname",
                table: "Benutzer",
                newName: "Name");
        }
    }
}
