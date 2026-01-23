using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Die1Er_Projektarbeit.Migrations
{
    /// <inheritdoc />
    public partial class NeueMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PasswordHash",
                table: "Benutzer");
        }
    }
}
